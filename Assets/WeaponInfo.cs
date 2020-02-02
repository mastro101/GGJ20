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
    private Action OnRepaired;

    private int currentStep=1;
    private SpriteRenderer spriteRenderer;

    void Start(){
        currentLife=maxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RegisterToOnRepairedEvent(Action action){
        OnRepaired=action;
    }

    public void ResetEvents(){
        currentLife=maxLife;
        OnRepaired=null;
    }

    public void OnHit(){
        currentLife--;
        if(sequence.Count!=0 && counter!=sequence.Count)
            ChangeSprite();

        if(currentLife<=0)
            OnRepaired();
    }

    int counter=1;
    private void ChangeSprite(){
        if(currentStep==counter && currentLife<=(maxLife-counter*(maxLife/sequence.Count))){
            spriteRenderer.sprite=sequence[currentStep];
            currentStep++;
            counter++;
        }   
    }
}
