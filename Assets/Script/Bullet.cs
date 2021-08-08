using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletDamage;



    // Start is called before the first frame update
    void Start()
    {
        //add force to move up
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //call OnDameged of Block class if bullet touch block
        if (other.CompareTag("Block"))
        {

            other.SendMessageUpwards("OnDamaged", bulletDamage);
            //destroy bullet
            Destroy(gameObject);
        }

        //destroy bullet when it out of scene view
        if (other.CompareTag("outScreen"))
        {
            Destroy(gameObject);
        }

        if (!other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    





}
