using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int lives = 10;
    public TMP_Text waveText;
    public TMP_Text livesText;
    public GameObject bossText;
    public GameObject waveUpdateText;

    //Variables for the waves
    public GameObject[] enemyWaves;
    private int wave = 0;
    private int lastWave = 5;
    private bool hasNoEnemies = false;
    private bool gameOver = false;

    //Variables needed for the AsteroidSpawnWaves
    public GameObject[] asteroid;
    public float asteroidWait = 2.0f;
    public float waveWait = 4.0f;
    public Vector3 spawnAsteroidLocation;
    

    // Start is called before the first frame update
    void Start()
    {
        bossText.SetActive(false);
        waveUpdateText.SetActive(false);
        waveText.text = "Wave: " + wave;
        livesText.text = "Lives: " + lives;
        StartCoroutine(SpawnWaves());
        StartCoroutine(AsteroidSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        // check for game over on the last wave
        if (wave == lastWave){
            if(GameObject.FindGameObjectWithTag("Enemy") == null){
                GameOver();
            }
        }
        
        if (wave < lastWave && wave > 0 && hasNoEnemies == false){
            if(GameObject.FindGameObjectWithTag("Enemy") == null){
                hasNoEnemies = true;
                StartCoroutine(SpawnWaves());
            }
        }
    }

    private void GameOver(){
        SceneManager.LoadScene("GameOver");
        gameOver = true;
    }

    private void YouLost(){
        SceneManager.LoadScene("YouLost");
    }

    public void LoseLife(){
        lives--;
        livesText.text = "Lives: " + lives;

        if(lives <= 0){
            YouLost();
        }
    }

    //spawn waves
    private IEnumerator SpawnWaves (){
        if (wave == lastWave-1){
            bossText.SetActive(true);
            yield return new WaitForSeconds (waveWait);
            bossText.SetActive(false);
        } 
        else{
            waveUpdateText.SetActive(true);
            yield return new WaitForSeconds (waveWait);
            waveUpdateText.SetActive(false);
        }

        Instantiate(enemyWaves[wave]);
        wave++;
        waveText.text = "Wave: " + wave;
        hasNoEnemies = false; 

        
    }

    // spawn asteroids until the game is over
    private IEnumerator AsteroidSpawn (){
        while(!gameOver){
            Vector3 spawnPosition = new Vector3(spawnAsteroidLocation.x, Random.Range(-spawnAsteroidLocation.y, spawnAsteroidLocation.y), spawnAsteroidLocation.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(asteroid[Random.Range(0, 4)], spawnPosition, spawnRotation);
            yield return new WaitForSeconds (asteroidWait);
        }
    }
}
