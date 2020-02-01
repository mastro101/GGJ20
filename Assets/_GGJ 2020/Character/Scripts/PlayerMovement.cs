using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [Space]
    [Header("Data")]
    [SerializeField] float speed;

    public int controllerInput;

    Vector2 direction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
            SetController.SetControllerToPlayer(this);

        MovementHandler();
    }

    Vector2 MovementVelocity()
    {
        Vector2 stickDirection;
        if (controllerInput != 0)
        {
            stickDirection = new Vector2(Input.GetAxis("Horizontal" + controllerInput.ToString()), Input.GetAxis("Vertical" + controllerInput.ToString()));
        }
        else
        {
            stickDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        direction = stickDirection.normalized;
        return direction * speed * Time.deltaTime;
    }

    void MovementHandler()
    {
        rigidbody2D.MovePosition(Vector2Utility.ConvertV3InV2(transform.position) + MovementVelocity());
    }
}