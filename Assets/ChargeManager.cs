using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeManager : MonoBehaviour
{

    // public float maxRange;
    // public int steps;

    // private float totalAngle;
    // private Vector2 prevDir;
    // private bool isRotating;

    // private float status=0;

    public Sprite HitSprite;
    public Sprite IdleSprite;
    public float timeBetweenHit;

    public float timeBetweenAnim;

    public List<Sprite> sequences;
    public List<float> timesForAnim;

    private SpriteRenderer spriteRenderer; 

    private int step=0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private float timeBetweenCharge=0;
    private float timeFromStart=0;
    void Update()
    {
        if(isAttacking) return;

        if(Input.GetAxis("Vertical1")<0){    
           if(timeFromStart==0)     
               spriteRenderer.sprite=sequences[0];
           timeFromStart+=Time.deltaTime;
           timeBetweenCharge+=Time.deltaTime;
           ChangeAnimation();
        } else if(timeFromStart>=timesForAnim[0]){
            StartCoroutine(HitAndReset());
        }else{
            timeFromStart=0;
            timeBetweenCharge=0;
            step=0;
            spriteRenderer.sprite=IdleSprite;
        }

        // var currentDir=new Vector3(Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
        // if(!isRotating){
        //     if(IsJoystickInRange(currentDir))
        //         StartCoroutine(GetAngles());
        // }else{
        //     if(!IsJoystickInRange(currentDir))
        //         StartCoroutine(HitAndReset());
        // }
            
    }

    private void ChangeAnimation(){

        if(timeBetweenCharge>=timesForAnim[step] && step<4){
             spriteRenderer.sprite=sequences[1+(step-1)];
             step++;
             timeBetweenCharge-=timeBetweenAnim;
        }

    }
  
    bool isAttacking=false;
    private IEnumerator HitAndReset(){
        isAttacking=true;
        spriteRenderer.sprite=HitSprite;
        yield return new WaitForSeconds(timeBetweenHit);  
        Reset();
        isAttacking=false;
    }

    private void Reset(){
        timeFromStart=0;
        timeBetweenCharge=0;
        step=0;
        spriteRenderer.sprite=IdleSprite;
    }

    // private IEnumerator GetAngles(){
    //     isRotating=true;
    //     prevDir=new Vector3(Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
    //     while(true){
    //         var currentDir=new Vector3(Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
    //         float newAngle=Vector2.Angle(prevDir,currentDir);

    //         prevDir = new Vector2(currentDir.x,currentDir.y);

    //         totalAngle+=newAngle;
    //         Debug.Log("Total angle: "+totalAngle);
    //         ChangeSprite(totalAngle);

    //         yield return new WaitForSeconds(0.4f);
    //     }

        
    // }

    // private IEnumerator HitAndReset(){
    //     if(totalAngle<angleForAnimation[0])
    //         yield return null;

    //     spriteRenderer.sprite=HitSprite;
    //     yield return new WaitForSeconds(timeBetweenHit);  
    //     spriteRenderer.sprite=IdleSprite;
    //     isRotating=false;
    //     totalAngle=0;
    //     status=0;
    // }

    // private void ChangeSprite(float angle){
    //     if(status==0 && angle>=angleForAnimation[0]){
    //         status++;
    //         spriteRenderer.sprite=sequences[0];
    //     } else if(status==1 && angle>=angleForAnimation[1]){
    //         status++;
    //         spriteRenderer.sprite=sequences[1];
    //     }else if(status==2 && angle>=angleForAnimation[2]){
    //         status++;
    //         spriteRenderer.sprite=sequences[2];
    //     }else if(status==3 && angle>=angleForAnimation[3]){
    //         status++;
    //         spriteRenderer.sprite=sequences[3];
    //     }

    // }

    // private bool IsJoystickInRange(Vector2 currentDir){
    //     return currentDir.magnitude>0.5f;
    // }
}
