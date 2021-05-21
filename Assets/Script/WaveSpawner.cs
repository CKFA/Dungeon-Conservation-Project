using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public bool isSpawning = true;
    public static int EnemiesAlive = 0;
    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countDown = 2f;

    public Text waveCountDownTopic;
    public Text waveCountDownText;

    private int waveIndex = 0;
    public Text waveIndexUi;
    public HpUI hpUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning) // if spawning
        {
            hpUI.isEnabled = true;
            if (countDown <= 0f) // less than 0 second , next wave go
            {
                StartCoroutine(SpawnWave());
                countDown = timeBetweenWaves;
            }
            countDown -= Time.deltaTime;
            countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
            waveCountDownText.text = Mathf.Round(countDown).ToString();
        }
        else // if not spawning
        {
            hpUI.isEnabled = false;
            waveCountDownTopic.color = new Color(0f, 0f, 0f, 0);
            waveCountDownText.color = new Color(0f, 0f, 0f, 0);
        }
        waveIndexUi.text = PlayerStats.waves.ToString();
    }

    IEnumerator SpawnWave()
    {
        
        Wave wave = waves[waveIndex];
        PlayerStats.waves++;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;


        if (waves.Length == waveIndex)
        {
            waveIndex = 0; // debug
            Debug.Log("Wave loop reaches max");
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
