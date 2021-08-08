using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Block : MonoBehaviour
{

    [SerializeField]
    private TextMeshPro textHP;
    [SerializeField]
    private float blockHP = 99;
    [SerializeField]
    private float fallingSpeed = 20.0f;
    private float timer;

    //block explosive animation
    [SerializeField]
    private GameObject blockPrefab;

    //block explosive sound
    [SerializeField]
    private AudioSource explosive;

    //hitting sound
    [SerializeField]
    private AudioSource hittingSound;

    private bool colorToggle;

    GameObject player;

    private int currentLostCount;
    private int previousLostCount;
    private int lostCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        colorToggle = false;
        textHP.text = blockHP.ToString();
        timer = fallingSpeed;

        currentLostCount = 0;
        previousLostCount = PlayerPrefs.GetInt("lostCount");
        Debug.Log("pre " + previousLostCount);
    }

    // Update is called once per frame
    void Update()
    {
        //block move down every 10s
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
            timer = fallingSpeed;

            //change block color
            if (!colorToggle)
            {
                GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                colorToggle = true;
            }
            else
            {
                GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                colorToggle = false;
            }
        }


    }


    public void OnDamaged(float damage)
    {

        if (blockHP > 0)
        {
            // reduce hp by bullet's damage
            blockHP -= damage;
            //play hitting sound
            hittingSound.Play();

            textHP.text = blockHP.ToString();
        }

        if (blockHP <= 0)
        {
            //play exposive sound
            explosive.Play();
            //destroy block if block hp = 0
            Destroy(gameObject);
            Debug.Log("Destroy 1 object");

            //instantiate eplosive animation particle system
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
            //count player score
            BlockController.instance.score += 1;
            //number of block - 1
            BlockController.instance.blockCount -= 1;
            if (BlockController.instance.blockCount <= 0)
            {
                //save lostCount
                currentLostCount += 1;
                countLost();

                //call method check game over
                setGameOver();
            }
        }

    }

    //set lost count for showing Ads
    public void countLost()
    {
        lostCount = previousLostCount + currentLostCount;
        PlayerPrefs.SetInt("lostCount", lostCount);
        PlayerPrefs.Save();
    }

    public void setGameOver()
    {

        //show ads every player lost 2 times
        if (lostCount % 2 != 0)
        {
            //stop the game
            Time.timeScale = 0;
            //show pop up after all block have been destroyed
            BlockController.instance.ActiveWinPopUp();

        }
        else
        {
            //stop the game
            Time.timeScale = 0;
            //show Ads
            PlayerController.playerController.RequestInterstitial();
            PlayerController.playerController.GameOver();

            //show pop up after all block have been destroyed
            BlockController.instance.ActiveWinPopUp();

        }

    }

    //Player lose if touch blocks
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //save lost count
            currentLostCount += 1;
            countLost();

            Debug.Log("cur " + currentLostCount);

            //destroy player
            Destroy(player);

            //call method check game over
            setGameOver();
            Debug.Log("lost " + lostCount);

        }
    }

}
