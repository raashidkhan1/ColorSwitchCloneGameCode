using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
   public GameObject[] obstaclePrefabs;
   public GameObject player;
   private int _obstacleDistance = 7;
   private string _lastObstacleTag = "";
   private List<GameObject> _spawnedObstacles;
   public int obstaclesForLevel;
   private int _obstacleCountForLevel = 0;
   public GameObject finishObjectPrefab;
   private bool _levelEnded;
   private float _crossObstacleOffset;
   

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, player.transform.position.y + 2, 0);
        _crossObstacleOffset = UnityEngine.Random.value > 0.5f ? 1.5f : -1.5f;
        InitializeInitialObstacles();
        _spawnedObstacles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_obstacleCountForLevel < obstaclesForLevel && !_levelEnded)
        {
            SpawnObstacles();
        }
        
        if(_obstacleCountForLevel == obstaclesForLevel)
        {
            SpawnFinishObject();
            _obstacleCountForLevel = 0;
            _levelEnded = true;
        }
        CleanUpObstacles();
    }

    void InitializeInitialObstacles()
    {
        // Spawn the initial 2 obstacles
        for (int i = 0; i < 2; i++)
        {
            SpawnObstacles();
        }
    }

    public void SpawnObstacles()
    {
        // spawn 1 obstacle at random on the y axis after the player has moved up and cleared the previous obstacle,
        // keep the count of obstacles spawned to max 2 and spawn above the topmost obstacle, don't spawn the colorswitcher obstacle again if its the last topmost obstacle and if its crossobstacle spawn it to a bit left or right of y axis of the player
        if (player && player.transform.position.y > transform.position.y)
        {
            int randomIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = obstaclePrefabs[randomIndex];
            if (obstacle.CompareTag("ColorSwitcher") && _lastObstacleTag == "ColorSwitcher")
            {
                randomIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
                obstacle = Instantiate(obstaclePrefabs[randomIndex]);
            }
            else if (obstacle.CompareTag("CrossObstacle"))
            {
                PositionCrossObstacle(obstacle);
            }
            else if (obstacle.CompareTag("TriangleObstacle") && TriangleObstacleHasNoPlayerColor())
            {
                    Debug.Log("Triangle without blue");
                    randomIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
                    GameObject newObstacle = obstaclePrefabs[randomIndex];
                    while(newObstacle.CompareTag("TriangleObstacle") && TriangleObstacleHasNoPlayerColor())
                    {
                        randomIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
                        newObstacle = obstaclePrefabs[randomIndex];
                    }
                    
                    PositionObstacle(newObstacle);
            }
            else
            {
                PositionObstacle(obstacle);
            }
            _obstacleCountForLevel++;
            _lastObstacleTag = obstacle.tag;
        }
    }
    
    private void PositionCrossObstacle(GameObject obstacle)
    {
        _crossObstacleOffset = 1.5f;
        Debug.Log("Cross Obstacle Offset: " + _crossObstacleOffset +" "+ player.transform.position.x);
        GameObject updatedObstacle = Instantiate(obstacle, new Vector3(player.transform.position.x + _crossObstacleOffset, transform.position.y + _obstacleDistance, player.transform.position.z), Quaternion.identity);
        transform.position = new Vector3(0, transform.position.y + _obstacleDistance, 0); // move the spawner up
        _spawnedObstacles.Add(updatedObstacle);
    }
    
    private bool TriangleObstacleHasNoPlayerColor()
    {
        return player.GetComponent<SpriteRenderer>().color.IsApproximately(PlayerController.Blue);
    } 
    
    private void PositionObstacle(GameObject obstacle)
    {
        GameObject updatedObject = Instantiate(obstacle, new Vector3(player.transform.position.x, transform.position.y + _obstacleDistance, player.transform.position.z), Quaternion.identity);
        transform.position = new Vector3(0, transform.position.y + _obstacleDistance, 0); // move the spawner up
        _spawnedObstacles.Add(updatedObject);
        
    }

    public void SpawnFinishObject()
    {
        // spawn the finish object at the topmost y axis of the player and end the level
        GameObject finishObject = Instantiate(finishObjectPrefab);
        finishObject.transform.position = new Vector3(player.transform.position.x, transform.position.y + _obstacleDistance, player.transform.position.z);
        transform.position = new Vector3(0, transform.position.y + _obstacleDistance, 0);
    }
    
    private void CleanUpObstacles()
    {
        // destroy the obstacles that are out of camera bounds
        if (player && _spawnedObstacles != null)
        {
            // Use a separate list to keep track of obstacles to remove
            List<GameObject> obstaclesToRemove = new List<GameObject>();

            foreach (var obstacle in _spawnedObstacles)
            {
                if (obstacle != null && obstacle.transform.position.y < player.transform.position.y - 10)
                {
                    obstaclesToRemove.Add(obstacle);
                }
            }

            // Destroy and remove obstacles outside the loop
            foreach (var obstacle in obstaclesToRemove)
            {
                if (obstacle != null)
                {
                    Destroy(obstacle);
                    _spawnedObstacles.Remove(obstacle);
                }
            }
        }
    }
}
