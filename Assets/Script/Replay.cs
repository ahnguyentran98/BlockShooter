using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Replay : MonoBehaviour
{


    
    private int scoreShow;
    public Text textScore;

    public Text textHighestScore;
    private int highestScoreShow;



    private void Update()
    {
        //highest score
        highestScoreShow = BlockController.instance.highestScore;
        textHighestScore.text = highestScoreShow.ToString();

        //score
        scoreShow = BlockController.instance.score;
        textScore.text = scoreShow.ToString();

    }

    //load Game after click replay button
    public void RestartGame()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

}
