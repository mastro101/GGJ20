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

    EffectConteiner effectConteiner;

    private float LeftLimit;
    private float RightLimit;
	
	private SoundManager soundManager;
	
    void Start()
    {
        startingPos=transform.position.x;
        LeftLimit=startingPos-range;
        RightLimit=startingPos;
		
		soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
        effectConteiner = GameObject.Find("EffectConteiner").GetComponent<EffectConteiner>();
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
                    if (effectConteiner != null)
                    {
                        effectConteiner.moveEffect.Play();
                        effectConteiner.moveEffect.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }else if(stickDirection.x<-0.5f && (currentPos-(range/steps))>=LeftLimit){
                    MovementHandler(-(range/steps));
                    StartCoroutine(AllowNextMovement());
                    if (effectConteiner != null)
                    {
                        effectConteiner.moveEffect.Play();
                        effectConteiner.moveEffect.GetComponent<SpriteRenderer>().flipX = true;
                    }
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
		int n=Random.Range(0,2);
		switch(n){
			case 0:
			soundManager.Play("ScorrimentoArma1");
			break;
			case 1:
			soundManager.Play("ScorrimentoArma1");
			break;
			case 2:
			soundManager.Play("ScorrimentoArma1");
			break;
		};
		
    }   
    
}
