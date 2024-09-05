using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int score = 0;
    public GameObject yourScore;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = ""+ score;
    }
    
    // Utility function to increment the score
    public void IncrementScore()
    {
        score++;
        GetComponent<TMPro.TextMeshProUGUI>().text = ""+ score;
        yourScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Your Score: "+ score;
        
    }
    
    // Utility function to reset the score
    public void ResetScore()
    {
        score = 0;
        GetComponent<TMPro.TextMeshProUGUI>().text = ""+ score;
        yourScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Your Score:"+ score;
    }
    
}
