using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public static HitManager SharedInstance;
    public float hitRange;
    public CameraShake cameraShake;
    private TargetSpawner activeTargetSpawner;
    CustomersQueue customersQueue;
    WeaponInfo weapon;

    public System.Action<WeaponInfo> OnCorrectHit;
    public System.Action<WeaponInfo> OnCompletWeapon;

    public void SetTargetSpawner(TargetSpawner targetSpawner){
        activeTargetSpawner=targetSpawner;
    }

    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        customersQueue = FindObjectOfType<CustomersQueue>();
        //weapon = customersQueue.NextCustomer().currentSword;
    }

    public void OnHit(){
        if(activeTargetSpawner==null) return;
        var target = activeTargetSpawner.GetActiveTarget();     
        
        if(PointInsideHitArea(target.transform.position)){
            Debug.Log("COLPITO");
            weapon.currentLife++;
            OnCorrectHit?.Invoke(weapon);
            if (weapon.currentLife == weapon.maxLife)
            {
                OnComplete();
            }

        }else{
            Debug.Log("MISS");
        }
        
        cameraShake.StartXShake();
        cameraShake.StartYShake();
        activeTargetSpawner.SpawnAfterDelay(1);
    }

    void OnComplete()
    {
        OnCompletWeapon?.Invoke(weapon);
        //weapon = customersQueue.NextCustomer().currentSword;
    }


    bool PointInsideHitArea(Vector3 targetPosition) {
        return Vector3.Distance(targetPosition, transform.position) < hitRange;
    }
}
