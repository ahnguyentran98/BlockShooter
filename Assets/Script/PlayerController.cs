using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class PlayerController : MonoBehaviour
{
    private Vector3 destination;

    public bool isShooting;

    [SerializeField]
    private float fireRate = 0.2f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private AudioSource shootSFX;

    private InterstitialAd interstitial;

    public static PlayerController playerController;




    // Start is called before the first frame update
    void Start()
    {
        //shooting at begining
        isShooting = true;
        //shoot sound volume
        shootSFX.volume = 0.2f;

        if (playerController == null)
        {
            playerController = this;
        }


        MobileAds.Initialize(initStatus => { });

    }

    void Update()
    {
       
    }


    private void FixedUpdate()
    {
        //move Player due Mouse position
        if (Input.GetMouseButton(0))
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, new Vector3(destination.x, destination.y, transform.position.z), 0.1f);
        }
        if (isShooting)
        {

            StartCoroutine(Shoot());

        }
        if (BlockController.instance.blockCount <= 0)
        {
            //stop game when destroy all blocks
            Time.timeScale = 0;
        }
    }

    IEnumerator Shoot()
    {
        //instatiate bullet from SpawnPoitn position
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        //play bullet sound
        shootSFX.Play();

        //stop spawn bullet for fireRate time
        isShooting = false;
        yield return new WaitForSeconds(fireRate);
        //spawn bullet again
        isShooting = true;


    }

    //create InterstitialAD
    public void RequestInterstitial()
    {

        string adUnitId = "ca-app-pub-5616151138236549";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    //show add when game over
    public void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

}
