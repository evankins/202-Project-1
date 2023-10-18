using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // ========== Fields ==========

    [SerializeField]
    SpriteRenderer enemyPrefab;
    [SerializeField]
    SpriteRenderer bulletPrefab;

    [SerializeField]
    List<Sprite> enemyImages = new List<Sprite>();

    List<SpriteRenderer> spawnedItems = new List<SpriteRenderer>();

    public const float TimeBetweenSpawns = 5f;
    float timeRemaining = TimeBetweenSpawns;


    protected SpawnManager() { }


    void Start()
    {
        SpawnEnemy();
    }


    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            timeRemaining = TimeBetweenSpawns;
        }
    }

    public SpriteRenderer Spawn(SpriteRenderer prefab)
    {
        return Instantiate(prefab);
    }

    public void SpawnEnemy()
    {
        SpriteRenderer spawnedEnemy = Spawn(enemyPrefab);
        spawnedItems.Add(spawnedEnemy);

        // Set position
        Vector2 spawnPosition;
        spawnPosition.x = 
        spawnPosition.y = Random.Range(-2f, 2f);


        spawnedEnemy.transform.position = spawnPosition;

        // Picking a random animal
        float randValue = Random.value;

        #region Random
        /*
        // Elephant: 25%
        if (randValue < 0.25f)
        {
            spawnedItems[i].sprite = enemyImages[0];
        }
        // Turtle: 20%
        else if (randValue < 0.45f)
        {
            spawnedItems[i].sprite = enemyImages[1];
        }
        // Snail: 15%
        else if (randValue < 0.60f)
        {
            spawnedItems[i].sprite = enemyImages[2];
        }
        // Octopus: 10%
        else if (randValue < 0.70f)
        {
            spawnedItems[i].sprite = enemyImages[3];
        }
        // Kangaroo: 30%
        else
        {
            spawnedItems[i].sprite = enemyImages[4];
        }
        */
        #endregion

    }

    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);

        float gaussValue =
                Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
                Mathf.Sin(2.0f * Mathf.PI * val2);

        return mean + stdDev * gaussValue;
    }

    public void DestroyAnimals()
    {
        // for better code, look up Unity object pooling
        // Eric Baker has a public GitHub Schump that uses object pooling in the SpawnManager
        // * good to show off for portfolios

        foreach (SpriteRenderer animal in spawnedItems)
        {
            // if you just do animal, you only destroy the spriteRenderer
            // any component can reference the GameObject it's attached to
            Destroy(animal.gameObject);
        }

        spawnedItems.Clear();
    }
}