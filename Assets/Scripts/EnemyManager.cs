using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private bool allowSpawn;
    public int numEnemies;
    private int maxEnemies;
    private int wave;
    private Transform spawnPoint;
    private float shortestDistance;

    public Transform northSpawn, eastSpawn, southSpawn, westSpawn;
    public Player player;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        allowSpawn = false;
        numEnemies = 0;
        maxEnemies = 2;
        wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowSpawn == false && player.totalOre >= 2800)
        {
            allowSpawn = true;
            StartCoroutine(SpawnEnemies());
        }
        
    }

    private void FindClosest()
    {
        shortestDistance = Vector2.Distance(player.transform.position, northSpawn.position);
        spawnPoint = northSpawn;

        if (shortestDistance > Vector2.Distance(player.transform.position, eastSpawn.position))
        {
            shortestDistance = Vector2.Distance(player.transform.position, eastSpawn.position);
            spawnPoint = eastSpawn;
        }

        if (shortestDistance > Vector2.Distance(player.transform.position, southSpawn.position))
        {
            shortestDistance = Vector2.Distance(player.transform.position, southSpawn.position);
            spawnPoint = southSpawn;
        }

        if (shortestDistance > Vector2.Distance(player.transform.position, westSpawn.position))
        {
            shortestDistance = Vector2.Distance(player.transform.position, westSpawn.position);
            spawnPoint = westSpawn;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            FindClosest();

            while (numEnemies < maxEnemies)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                numEnemies++;

                yield return null;
            }

            if (wave < 20)
            {
                wave++;
                maxEnemies = (int)Mathf.Round(wave * 2.5f);
            }

            while (numEnemies > 0)
            {
                yield return null;
            }

            for (int i = 0; i < 120; i++)
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
