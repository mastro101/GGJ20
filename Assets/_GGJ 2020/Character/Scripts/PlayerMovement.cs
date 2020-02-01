using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public int controllerInput;

    Vector2 direction;
    private Rigidbody2D rb2D;
    
    private void Start() {
       rb2D=GetComponent<Rigidbody2D>();
    }

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
        rb2D.MovePosition(Vector2Utility.ConvertV3InV2(transform.position) + MovementVelocity());
    }
}