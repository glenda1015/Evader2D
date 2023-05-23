using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserController : MonoBehaviour
{
    public GameObject redLaserHit; //red particle effect explosion
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }

    // //method is going to get called when ever there is a collision that has the IsTrigger checkbox checked
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Asteroid")
        {
            Instantiate(redLaserHit, transform.position, transform.rotation);
            Destroy(gameObject); //destroy
        }
        if (other.tag == "Enemy")
        {
            Instantiate(redLaserHit, transform.position, transform.rotation);
            Destroy(gameObject); //destroy laser
            Destroy(other.gameObject);//destroy the enemy ship
        }
    }

     //method for the lasers of the player to become invisble and end its live
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
