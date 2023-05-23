using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Vector2 moveInput;
    GameController gameController;

    public float speed = 8.0f;
    public Transform upperLeftBounds, lowerRightBounds;

    public GameObject laserShot; //Prefab player laser to instantiate
    public GameObject redLaserHit; //red particle effect explosion

    public Transform firePoint; //Where to create the player laser

    public float shotDelay = 0.4f;
    float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        rb.velocity = moveInput * speed;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, upperLeftBounds.position.x, lowerRightBounds.position.x),
            Mathf.Clamp(transform.position.y, lowerRightBounds.position.y, upperLeftBounds.position.y),
            transform.position.z
        );

        //animator of spaceship
        anim.SetFloat("PlayerMovement", Input.GetAxisRaw("Vertical"));

        //fire when the button is clicked on one time
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(laserShot, firePoint.position, firePoint.rotation);
        }
        //fire when the button is pressed
        if (Input.GetButton("Fire1"))
        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0)
            {
                Instantiate(laserShot, firePoint.position, firePoint.rotation);
                shotTimer = shotDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {        
        if(other.gameObject.tag == "EnemyLaser")
        {
            //destroy health/life
            gameController.LoseLife();
            //particle effect
            Instantiate(redLaserHit, transform.position, transform.rotation);
        }
    }
}
