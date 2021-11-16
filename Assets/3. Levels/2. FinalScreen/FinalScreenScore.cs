using System.Collections;
using System.Collections.Generic;
using _3._Levels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreenScore : MonoBehaviour
{
    public Text scoreText;
    public Text statusText;
    
    // Start is called before the first frame update
    private void Start()
    {
        scoreText.text = LevelScore.Score;
        statusText.text = LevelScore.Status;
    }

    public void ReturnToBoot()
    {
        SceneManager.LoadScene(0);
    }
}
