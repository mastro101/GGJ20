using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    private bool isMoving;
    Vector2 direction;
    float startingPos;

    public float range;
    public float steps;

    private float maxRangeLeft;
    private float maxRangeRight;

    public float timeBeforeNextMovement=0;
    private Rigidbody2D rb2D;

    void Start()
    {
        startingPos=transform.position.x;
        maxRangeLeft=(startingPos-range/2);
        maxRangeRight=(startingPos+range/2);
        rb2D=GetComponent<Rigidbody2D>();
    }
  
    void Update()
    {    
        var stickDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        HandleMovement(stickDirection);
    }

    void HandleMovement(Vector2 stickDirection){
        if(stickDirection.x!=0 && !isMoving){
            var currentPos=Mathf.Abs(transform.position.x);
            if(currentPos>=maxRangeLeft && currentPos<=maxRangeRight){
                if(stickDirection.x>0.5f && (currentPos+(range/steps))<maxRangeRight){
                    MovementHandler((range/steps));
                    StartCoroutine(AllowNextMovement());
                }else if(stickDirection.x<-0.5f && (currentPos-(range/steps))>maxRangeLeft){
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
        Vector2 xMovement=new Vector2(moveAmount,0);
        rb2D.MovePosition(Vector2Utility.ConvertV3InV2(transform.position) + xMovement);
    }   
    
}
