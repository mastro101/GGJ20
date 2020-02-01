using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    private bool isMoving;
    float startingPos;

    public float range;
    public float steps;

    public float timeBeforeNextMovement=0;

    private float LeftLimit;
    private float RightLimit;
    void Start()
    {
        startingPos=transform.position.x;
        LeftLimit=startingPos-range;
        RightLimit=startingPos;
    }
  
    void Update()
    {    
        var stickDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        HandleMovement(stickDirection);
    }


    void HandleMovement(Vector2 stickDirection){
        if(stickDirection.x!=0 && !isMoving){
            var currentPos=transform.position.x;
            if(currentPos>=LeftLimit && currentPos<=RightLimit){
                if(stickDirection.x>0.5f && (currentPos+(range/steps))<=RightLimit){
                    MovementHandler((range/steps));
                    StartCoroutine(AllowNextMovement());
                }else if(stickDirection.x<-0.5f && (currentPos-(range/steps))>=LeftLimit){
                    MovementHandler(-(range/steps));
                    StartCoroutine(AllowNextMovement());
                }                   
            }           
        }
    }

    private IEnumerator AllowNextMovement(){
        isMoving=true;
        yield return new WaitForSeconds(timeBeforeNextMovement);  
        isMoving=false;
    }

    void MovementHandler(float moveAmount)
    {
        transform.position+=new Vector3(moveAmount,0,0);
    }   
    
}
