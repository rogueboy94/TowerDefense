using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waves;
    
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 10f;

    public Text waveCountdownText;

	[HideInInspector]
    public int waveNumber = 0;

    void Update()
    {
        if(EnemiesAlive > 0)
            return;
        
        if(countdown <= 0f)
        {
            //Starting to Spawn Enemies
            StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);

		if (waveNumber == waves.Length)
		{
			GameManager.GameWon = true;
			this.enabled = false;
		}
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];
		Debug.Log(waveNumber);

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveNumber++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
