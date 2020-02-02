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
	
	private SoundManager soundManager;

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
		soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void SetWeapon(WeaponInfo weapon){
        this.weapon=weapon;
    }

    public void OnHit(){
        if(activeTargetSpawner==null) return;
        var target = activeTargetSpawner.GetActiveTarget();     
        
        if(PointInsideHitArea(target.transform.position)){
            Debug.Log("COLPITO");
			
			//play suono relativo alla corretta potenza
			soundManager.Play("Colpo1");			
            weapon.OnHit();

        }else{
            Debug.Log("MISS");
			soundManager.Play("ColpoSbagliato");
        }
        
        cameraShake.StartXShake();
        cameraShake.StartYShake();
        activeTargetSpawner.SpawnAfterDelay(1);
    }

    bool PointInsideHitArea(Vector3 targetPosition) {
        return Vector3.Distance(targetPosition, transform.position) < hitRange;
    }
}
