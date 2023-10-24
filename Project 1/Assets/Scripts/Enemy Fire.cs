using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer enemyBulletPrefab;


    [SerializeField]
    float shotCooldown;
    float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        // no matter the cooldown, tanks shoot shortly after spawning
        timeRemaining = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0) 
        {
            SpawnManager.Instance.SpawnBullet(this.transform.position, enemyBulletPrefab, true);
            timeRemaining = shotCooldown;
        }
    }
}
