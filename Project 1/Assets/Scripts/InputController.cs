using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    MovementController movementController;

    public const float ShotCooldown = 0.2f;
    float timeRemaining = ShotCooldown;

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
        if (timeRemaining < 0)
        {
            SpawnManager.Instance.SpawnBullet(this.transform.position);
            timeRemaining = ShotCooldown;
        }
    }
}
