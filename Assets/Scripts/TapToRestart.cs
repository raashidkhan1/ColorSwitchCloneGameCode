using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToRestart : MonoBehaviour
{
    public GameObject scorer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // reload the scene when the player taps the screen
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            gameObject.SetActive(false);
            scorer.GetComponent<ScoreHandler>().ResetScore();
        } 
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // load the default scene when the player presses the escape key
            UnityEngine.SceneManagement.SceneManager.LoadScene("Default");
        }
    }
}
