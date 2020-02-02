using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitManager : MonoBehaviour
{
    public WeaponLifeUI weaponLifeUI;
    public static HitManager SharedInstance;
    public float hitRange;
    public CameraShake cameraShake;
    private TargetSpawner activeTargetSpawner;
    CustomersQueue customersQueue;
    WeaponInfo weapon;
    
    public ChargeManager chargeManager;
	
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
		soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void SetWeapon(WeaponInfo weapon){
        this.weapon=weapon;
    }

    public void OnHit(){
        if(activeTargetSpawner==null) 
            return;
        
        cameraShake.StartXShake();
        cameraShake.StartYShake();

        var target = activeTargetSpawner.GetActiveTarget();     

        if(PointInsideHitArea(target.transform.position)){
            Debug.Log("COLPITO");
			
			//play suono relativo alla corretta potenza
			soundManager.Play("Colpo1");			
            weapon.OnHit();
            weaponLifeUI.SetFillAmount(weapon.GetLifeAmount());

        }else{
            Debug.Log("MISS");
			soundManager.Play("ColpoSbagliato");
        }
        
        if(!weapon.IsFixed())
            activeTargetSpawner.SpawnAfterDelay(1);
    }

    bool PointInsideHitArea(Vector3 targetPosition) {
        return Vector3.Distance(targetPosition, transform.position) < hitRange;
    }
}
