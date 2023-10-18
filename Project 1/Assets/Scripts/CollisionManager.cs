using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // ========== Fields ==========

    [SerializeField]
    SpriteInfo player;

    List<SpriteInfo> enemyCollidables = new List<SpriteInfo>();
    List<SpriteInfo> bulletCollidables = new List<SpriteInfo>();
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
                // ===== Add damage to player =======
            }

            foreach (SpriteInfo bullet in bulletCollidables)
            {
                if (AABBCheck(enemy, bullet))
                {
                    toBeDestroyed.Add(enemy);
                    toBeDestroyed.Add(bullet);
                }
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
        else
        {
            bulletCollidables.Remove(sprite);
        }
    }
}
