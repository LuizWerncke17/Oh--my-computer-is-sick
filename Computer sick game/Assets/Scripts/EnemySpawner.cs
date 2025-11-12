using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] commonEnemyPrefabs; // inimigos básicos
    [SerializeField] private GameObject[] eliteEnemyPrefabs;  // inimigos elite
    [SerializeField] private GameObject[] bosses;             // inimigos chefes

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float baseEnemiesPerSecond = 0.7f;
    [SerializeField] private float maxEnemiesPerSecond = 3f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private List<GameObject> currentWaveEnemies = new List<GameObject>();

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        float currentEnemiesPerSecond = CalculateEnemiesPerSecond();

        if (timeSinceLastSpawn >= (1f / currentEnemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        if (currentWaveEnemies.Count == 0) return;

        int index = Random.Range(0, currentWaveEnemies.Count);
        GameObject enemyPrefab = currentWaveEnemies[index];
        currentWaveEnemies.RemoveAt(index);

        Instantiate(enemyPrefab, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWaveEnemies = GetEnemiesForWave(currentWave);

        enemiesLeftToSpawn = currentWaveEnemies.Count;
        isSpawning = true;
        timeSinceLastSpawn = 0f;

    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        enemiesLeftToSpawn = 0;
        enemiesAlive = 0;

        currentWave++;
        LevelManager.main.wave++;

        if (currentWave > 50)
        {
            LevelManager.main.wave = 50;
            LevelManager.main.money = 0;
            Debug.Log("Você venceu o jogo!");
            return;
        }
        else{
            StartCoroutine(StartWave());
        }
    }

    private List<GameObject> GetEnemiesForWave(int wave)
    {
        List<GameObject> enemies = new List<GameObject>();

        if (wave == 20)
        {
            enemies.Add(bosses[0]);
            return enemies;
        }
        else if (wave == 50)
        {
            enemies.Add(bosses[1]); //ondas que vem só o voz na lista de inimigos, dai retorna só eles sozinhos
            return enemies;
        }

        if (wave < 40) //inimigos comuns que vão até a onda 40
        {
            enemies.AddRange(commonEnemyPrefabs);
        }

        if (wave >= 21)
        {
            enemies.Add(eliteEnemyPrefabs[0]);
            if (wave % 5 == 0)
                enemies.Add(eliteEnemyPrefabs[0]); //inimigos elites que começam a aparecer a partir de suas ondas, e aparecem mais frequentemente a cada X ondas
        }

        if (wave >= 26)
        {
            enemies.Add(eliteEnemyPrefabs[1]);
            if (wave % 6 == 0)
                enemies.Add(eliteEnemyPrefabs[1]);
        }

        if (wave >= 31)
        {
            enemies.Add(eliteEnemyPrefabs[2]);
            if (wave % 7 == 0)
                enemies.Add(eliteEnemyPrefabs[2]);
        }

        if (wave >= 40 && wave % 5 == 0)
        {
            enemies.Add(bosses[0]);
            if (wave % 6 == 0)
                enemies.Add(eliteEnemyPrefabs[1]);
        }

        int totalEnemies = Mathf.RoundToInt(baseEnemies * Mathf.Pow(wave, difficultyScalingFactor)); //conta para a quantidade total de inimigos na onda

        List<GameObject> enemiesForThisWave = new List<GameObject>();

        for (int i = 0; i < totalEnemies; i++)
        {
            int randomIndex = Random.Range(0, enemies.Count);
            enemiesForThisWave.Add(enemies[randomIndex]); //cria uma lista randomica de inimigos que é possível ver no debug
        }

        return enemiesForThisWave;
    }

        private float CalculateEnemiesPerSecond()
    {
        float scaledRate = baseEnemiesPerSecond * Mathf.Pow(1.1f, currentWave - 1);
        return Mathf.Min(scaledRate, maxEnemiesPerSecond);
    }
}
