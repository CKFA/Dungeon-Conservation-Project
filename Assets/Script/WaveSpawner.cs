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
    [Range(20,100)]
    public int HpBuff = 1;
    public float timeBetweenWaves = 5f;
    private float countDown = 2f;

    public Text waveCountDownTopic;
    public Text waveCountDownText;

    private int waveIndex = 0;
    public Text waveIndexUi;
    
    public HpUI hpUI;
    public GameObject enemiesParent;
    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        countDown = timeBetweenWaves;
    }
    void Update()
    {
        if (isSpawning) // if spawning
        {
            if(hpUI !=null)
            {
                hpUI.isEnabled = true;
            }
            
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
            if(hpUI != null)
            {
                hpUI.isEnabled = false;
            }
            
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
        EnemyLevelUpper(wave.enemy);

        if (waves.Length == waveIndex)
        {
            waveIndex = 0; // debug
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

    void EnemyLevelUpper(GameObject enemy)
    {
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        enemyAI.hp = enemyAI.startHp + (PlayerStats.waves * HpBuff);
    }

    public void StartNextWave()
    {
        countDown = 0f;
    }
}
