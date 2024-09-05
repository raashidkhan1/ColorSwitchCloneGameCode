using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleHandler : MonoBehaviour
{
    public GameObject playerDeathEffect;
    public GameObject starCollectionEffect;
    public AudioClip playerDeathSound;
    public AudioClip starCollectionSound;
    public AudioClip colorSwitchSound;
    public Camera mainCamera;
    public GameObject gameOverPanel;
    public GameObject scorer;
    bool isGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        // hide game over panel at the start of the game
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        isGameOver = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player collides with obstacle, and their colors do not match, destroy player and instantiate player death effect at player's position
        // or if it goes out of camera lower bound, destroy player and instantiate player death effect
        GameObject collidedObstacle = collision.gameObject;
        if (!collidedObstacle.GetComponent<SpriteRenderer>().color.IsApproximately(GetComponent<SpriteRenderer>().color) && 
            !collidedObstacle.CompareTag("Star") && !collidedObstacle.CompareTag("ColorSwitcher"))
        {
            Debug.Log("Color Mismatch: "+ collidedObstacle.GetComponent<SpriteRenderer>().color + " != " + GetComponent<SpriteRenderer>().color);
            GameOverEffect(collidedObstacle.transform.position);
        }
        else if (collidedObstacle.CompareTag("Star"))
        {
            OnStarCollection(collidedObstacle);
        }
        else if (collidedObstacle.CompareTag("ColorSwitcher"))
        {
            Debug.Log("Color Switcher collided");
            OnCollisionWithColorSwitcher(collidedObstacle);
        }
        else if (collidedObstacle.CompareTag("Finish"))
        {
            Debug.Log("Finish collided");
            GameOverEffect(collidedObstacle.transform.position);
        }
        else
        {
            //let the player pass through the obstacle if their colors match
            Debug.Log("Player passed through obstacle");
        }
        
    }
    
    // destroy player ball, spawn game over effect at player's position then overlay game over screen
    private void GameOverEffect(Vector3 position)
    {
       if(!isGameOver)
       {
           isGameOver = true;
           Destroy(gameObject);
           Instantiate(playerDeathEffect, position, Quaternion.identity);
           AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);
           ShowGameOverPanel();
       }
    }

    private void OnStarCollection(GameObject star)
    {
        Debug.Log("Star Collected");
        Destroy(star);
        StarCollectionEffect(transform.position);
        scorer.GetComponent<ScoreHandler>().IncrementScore();
    }

    private void StarCollectionEffect(Vector3 position)
    {
        Instantiate(starCollectionEffect, position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(starCollectionSound, transform.position);
    }
    
    private void OnCollisionWithColorSwitcher(GameObject collidedObstacle)
    {
        Color[] colors = {PlayerController.Green, PlayerController.Red, PlayerController.Blue, PlayerController.Orange};
        Color randomColor = colors[UnityEngine.Random.Range(0, colors.Length)];
        while (randomColor == GetComponent<SpriteRenderer>().color)
        {
            randomColor = colors[UnityEngine.Random.Range(0, colors.Length)];
        }
        GetComponent<SpriteRenderer>().color = randomColor;
        AudioSource.PlayClipAtPoint(colorSwitchSound, transform.position);
        Destroy(collidedObstacle);
    }

    private void OnBecameInvisible()
    {
        // if player goes out of camera bounds, destroy player and instantiate player death effect at camera's lower bound
        GameOverEffect(new Vector3(transform.position.x, mainCamera.ViewportToWorldPoint(Vector3.zero).y, transform.position.z));
    }
    
    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            // Get the RectTransform component of the panel
            RectTransform panelRectTransform = gameOverPanel.GetComponent<RectTransform>();

            // Set the anchors to center
            panelRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            panelRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            // Set the pivot to center
            panelRectTransform.pivot = new Vector2(0.5f, 0.5f);

            // Set the anchored position to (0, 0) to center it on the screen
            panelRectTransform.anchoredPosition = Vector2.zero;
            
            gameOverPanel.SetActive(true);
        }
    }
    
}
