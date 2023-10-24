using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // ========== Fields ==========

    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    SpriteRenderer enemyBasePrefab;
    [SerializeField]
    SpriteRenderer enemyFastPrefab;
    [SerializeField]
    SpriteRenderer enemyHowitzerPrefab;
    [SerializeField]
    SpriteRenderer bulletPrefab;

    

    List<SpriteRenderer> spawnedObjects = new List<SpriteRenderer>();

    [SerializeField]
    float timeBetweenSpawns;
    float timeRemaining;

    // camera (bounds) values
    Camera cameraObject;
    float totalCamHeight;
    float totalCamWidth;


    protected SpawnManager() { }


    void Start()
    {
        cameraObject = Camera.main;
        totalCamHeight = cameraObject.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * cameraObject.aspect;

        timeRemaining = timeBetweenSpawns;

        SpawnEnemy();
    }


    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            SpawnEnemy();
            timeRemaining = timeBetweenSpawns;
        }
    }

    public SpriteRenderer Spawn(SpriteRenderer prefab)
    {
        return Instantiate(prefab);
    }

    public void SpawnEnemy()
    {
        SpriteRenderer spawnedEnemy;

        // Select a random enemy
        float randValue = Random.value;
        // Base: 60%
        if (randValue < 0.60f)
        {
            spawnedEnemy = Spawn(enemyBasePrefab);
        }
        // Fast: 20%
        else if (randValue < 0.80f)
        {
            spawnedEnemy = Spawn(enemyFastPrefab);
        }
        // Howitzer: 20%
        else
        {
            spawnedEnemy = Spawn(enemyHowitzerPrefab);
        }


        spawnedObjects.Add(spawnedEnemy);
        collisionManager.enemyCollidables.Add(spawnedEnemy.gameObject.GetComponent<SpriteInfo>());

        // Set position
        Vector2 spawnPosition;

        // Spawns enemies slightly off-screen
        spawnPosition.x = (totalCamWidth / 2f) * 1.1f;
        // Range from top to bottom of screen
        spawnPosition.y = Random.Range(-(totalCamHeight / 2) * 0.9f, (totalCamHeight / 2) * 0.9f);

        spawnedEnemy.transform.position = spawnPosition;
    }

    public void SpawnBullet(Vector3 position, SpriteRenderer bullet, bool isEnemyBullet)
    {
        SpriteRenderer spawnedBullet = Spawn(bullet);
        spawnedObjects.Add(spawnedBullet);

        // enemy bullets must be added to a different list
        if (isEnemyBullet)
        {
            collisionManager.enemyBulletCollidables.Add(spawnedBullet.gameObject.GetComponent<SpriteInfo>());
        }
        else
        {
            collisionManager.playerBulletCollidables.Add(spawnedBullet.gameObject.GetComponent<SpriteInfo>());
        }

        // Set position
        // Bullets go behind tanks
        position.z += 5f;
        spawnedBullet.transform.position = position;
    }

    public void DestroyObject(GameObject obj)
    {
        spawnedObjects.Remove(obj.GetComponent<SpriteRenderer>());
        collisionManager.RemoveCollisions(obj.GetComponent<SpriteInfo>());
        Destroy(obj);
    }
}