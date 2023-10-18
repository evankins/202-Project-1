using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // ========== Fields ==========

    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    SpriteRenderer enemyPrefab;
    [SerializeField]
    SpriteRenderer bulletPrefab;

    List<SpriteRenderer> spawnedObjects = new List<SpriteRenderer>();

    public const float TimeBetweenSpawns = 5f;
    float timeRemaining = TimeBetweenSpawns;

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

    public void SpawnBullet(Vector2 position)
    {
        SpriteRenderer spawnedBullet = Spawn(bulletPrefab);
        spawnedObjects.Add(spawnedBullet);
        collisionManager.bulletCollidables.Add(spawnedBullet.gameObject.GetComponent<SpriteInfo>());

        // Set position
        spawnedBullet.transform.position = position;
    }

    public void DestroyObject(GameObject obj)
    {
        spawnedObjects.Remove(obj.GetComponent<SpriteRenderer>());
        collisionManager.RemoveCollisions(obj.GetComponent<SpriteInfo>());
        Destroy(obj);
    }
}