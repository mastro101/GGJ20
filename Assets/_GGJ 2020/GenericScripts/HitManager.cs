using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public static HitManager SharedInstance;
    public float hitRange;
    public CameraShake cameraShake;
    private TargetSpawner activeTargetSpawner;

    public void SetTargetSpawner(TargetSpawner targetSpawner){
        activeTargetSpawner=targetSpawner;
    }

    void Awake()
    {
        SharedInstance = this;
    }

    public void OnHit(){
        var target = activeTargetSpawner.GetActiveTarget();     
        
        if(PointInsideHitArea(target.transform.position)){
            Debug.Log("COLPITO");
        }else{
            Debug.Log("MISS");
        }
        
        cameraShake.StartXShake();
        cameraShake.StartYShake();
        activeTargetSpawner.SpawnAfterDelay(1);
    }


    bool PointInsideHitArea(Vector3 targetPosition) {
        return Vector3.Distance(targetPosition, transform.position) < hitRange;
    }

}
