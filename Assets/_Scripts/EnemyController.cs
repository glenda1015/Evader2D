using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyLaser; //Prefab enemy laser to instantiate
    public Transform firePoint; //Where to create the enemy laser
    public float shotDelay = 0.4f;
    bool startFire;
    float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // fire the enemy laser depping on the delay
        shotTimer -= Time.deltaTime;
        if(shotTimer < 0.0f && startFire == true){
            shotTimer = shotDelay;
            Instantiate(enemyLaser, firePoint.position, firePoint.rotation);
        }
    }

    // start firing only when enemy becomes visible
    private void OnBecameVisible() {
        startFire = true;
    }
}
