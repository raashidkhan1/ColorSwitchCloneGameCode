using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    // load the scene with the given name
    public void LoadScene(string sceneName)
    {
        // load the scene with the given name
        SceneManager.LoadScene(sceneName);
    }
    
}
