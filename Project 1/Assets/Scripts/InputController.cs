using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    MovementController movementController;

    [SerializeField]
    SpriteRenderer playerBulletPrefab;

    [SerializeField]
    float shotCooldown = 0.3f;
    float timeRemaining;

    void Start()
    {
        timeRemaining = shotCooldown;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementController.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (timeRemaining <= 0)
        {
            SpawnManager.Instance.SpawnBullet(this.transform.position, playerBulletPrefab, false);
            timeRemaining = shotCooldown;
        }
    }
}
