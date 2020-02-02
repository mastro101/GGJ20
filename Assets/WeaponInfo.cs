using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class WeaponInfo : MonoBehaviour
{

    public List<Sprite> sequence;

    public Vector2 offset;
    public float Value;
    public int maxLife;
    private int currentLife;
    int counter=1;
    private Action OnRepaired;
    private bool isFixed;

    private int currentStep=1;
    private SpriteRenderer spriteRenderer;

    void Awake(){      
        spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    public void RegisterToOnRepairedEvent(Action action){
        OnRepaired=action;
    }

    public void Reset(){
        currentLife=maxLife;
        isFixed=false;
        OnRepaired=null;
        currentStep=counter=1;
        spriteRenderer.sprite=sequence[0];
    }

    public void OnHit(){
        currentLife--;
        if(sequence.Count!=0 && counter!=sequence.Count)
            ChangeSprite();

        if(currentLife<=0){
            OnRepaired();
            isFixed=true;
        }          
    }


    public float GetLifeAmount(){
        return (float)currentLife/maxLife;
    }

    public bool IsFixed(){
        return isFixed;
    }
    private void ChangeSprite(){
        if(currentStep==counter && currentLife<=(maxLife-counter*(maxLife/sequence.Count))){
            spriteRenderer.sprite=sequence[currentStep];
            currentStep++;
            counter++;
        }   
    }
}
