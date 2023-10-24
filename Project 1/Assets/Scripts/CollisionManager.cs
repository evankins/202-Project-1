using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // ========== Fields ==========

    [SerializeField] 
    UIManager uiManager;

    [SerializeField]
    SpriteInfo player;

    public List<SpriteInfo> enemyCollidables = new List<SpriteInfo>();

    public List<SpriteInfo> playerBulletCollidables = new List<SpriteInfo>();

    public List<SpriteInfo> enemyBulletCollidables = new List<SpriteInfo>();

    List<SpriteInfo> toBeDestroyed = new List<SpriteInfo>();

    // Update is called once per frame
    void Update()
    {
        // Enemies against player and bullets
        foreach (SpriteInfo enemy in enemyCollidables)
        {
            if (AABBCheck(enemy, player))
            {
                toBeDestroyed.Add(enemy);
                uiManager.DecreaseLives();
            }

            foreach (SpriteInfo bullet in playerBulletCollidables)
            {
                if (AABBCheck(enemy, bullet))
                {
                    toBeDestroyed.Add(enemy);
                    toBeDestroyed.Add(bullet);
                    uiManager.IncreaseScore(100);
                }
            }
        }

        // Enemy bullets against player
        foreach (SpriteInfo bullet in enemyBulletCollidables)
        {
            if (AABBCheck(bullet, player))
            {
                toBeDestroyed.Add(bullet);
                uiManager.DecreaseLives();
            }
        }

        // Clean out any bad sprites
        for (int i = 0; i < toBeDestroyed.Count; i++)
        {
            SpawnManager.Instance.DestroyObject(toBeDestroyed[i].gameObject);
        }
        toBeDestroyed.Clear();
    }

    bool AABBCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        // Check for collision
        if (spriteB.RectMin.x < spriteA.RectMax.x &&
            spriteB.RectMax.x > spriteA.RectMin.x &&
            spriteB.RectMax.y > spriteA.RectMin.y &&
            spriteB.RectMin.y < spriteA.RectMax.y
            )
        {
            return true;
        }


        return false;
    }

    public void RemoveCollisions(SpriteInfo sprite)
    {
        if (enemyCollidables.Contains(sprite))
        {
            enemyCollidables.Remove(sprite);
        }
        else if (enemyBulletCollidables.Contains(sprite))
        {
            enemyBulletCollidables.Remove(sprite);
        }
        else
        {
            playerBulletCollidables.Remove(sprite);
        }
    }
}
