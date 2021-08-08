using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public int blockCount = 0;
    public static BlockController instance;
    public GameObject WinPopUp;

    public int score;
    private int previousScore;

    public int highestScore;



    // Start is called before the first frame update
    void Start()
    {
        //take previousScore form PlayerPrefs to check with current score
        previousScore = PlayerPrefs.GetInt("highestScore");

        //use to set score = 0
        //previousScore = 0;


        PlayerPrefs.Save();

        if (instance == null)
        {
            instance = this;
        }

        //count number of child blocks
        blockCount = gameObject.transform.childCount;
        //log check
        Debug.Log("number block count" + blockCount);
    }

    public void ActiveWinPopUp()
    {
        //stop game when popUp active
        Time.timeScale = 0;
        WinPopUp.SetActive(true);
        WinUp();
    }
    public void WinUp()
    {
        //stop shooting while pop up showing
        PlayerController.playerController.isShooting = false;
        //instantiate pop up position
        Instantiate(WinPopUp, transform.position, Quaternion.identity);
    }





    // Update is called once per frame
    void Update()
    {
        //save current score to highestScore in PlayerPrefs
        PlayerPrefs.SetInt("highestScore", score);

        //check if current score < previous score or not
        if (previousScore > PlayerPrefs.GetInt("highestScore"))
        {
            PlayerPrefs.SetInt("highestScore", previousScore);
            PlayerPrefs.Save();

        }
        PlayerPrefs.Save();

        //set highestScore 
        highestScore = PlayerPrefs.GetInt("highestScore");
    }


}
