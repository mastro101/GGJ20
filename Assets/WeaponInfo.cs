using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class WeaponInfo : MonoBehaviour
{

    public Vector2 offset;
    public float Value;
    public int maxLife;
    private int currentLife;
    private Action OnRepaired;

    void Start(){
        currentLife=maxLife;
    }

    public void RegisterToOnRepairedEvent(Action action){
        OnRepaired=action;
    }

    public void ResetEvents(){
        OnRepaired=null;
    }

    public void OnHit(){
        currentLife--;
        if(currentLife<0)
            OnRepaired();
    }
}
