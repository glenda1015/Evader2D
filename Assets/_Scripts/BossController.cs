using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject greenEnemyLaser; //Prefab enemy laser to instantiate
    public GameObject redEnemyLaser; //Prefab enemy laser to instantiate
    public Transform firePoint1; //Where to create the enemy laser
    public Transform firePoint2; //Where to create the enemy laser
    public Transform firePoint3; //Where to create the enemy laser
    public Transform firePoint4; //Where to create the enemy laser

    public float shotDelay = 0.7f;
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
            Instantiate(greenEnemyLaser, firePoint1.position, firePoint1.rotation);
            Instantiate(greenEnemyLaser, firePoint2.position, firePoint2.rotation);
            Instantiate(redEnemyLaser, firePoint3.position, firePoint3.rotation);
            Instantiate(redEnemyLaser, firePoint4.position, firePoint4.rotation);
        }
    }

    // start firing only when enemy becomes visible
    private void OnBecameVisible() {
        startFire = true;
    }
}
