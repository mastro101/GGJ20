using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInfo : MonoBehaviour
{
    private int Intensity;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetIntensity(int value){
        Intensity=value;
    }

    public int GetIntensity(){
        return Intensity;
    }
}
