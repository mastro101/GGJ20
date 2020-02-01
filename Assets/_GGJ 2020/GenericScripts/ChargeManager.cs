using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeManager : MonoBehaviour
{
    [System.Serializable]
    public class MacroAnimation
    {
        public float speed;
        public List<Sprite> sequences;
        private int currentAnim=0;
        public bool isPlaying=false;
        public bool HasEnded=false;

        public int intensity;

        public Sprite GetCurrentSequence(){
            var currentSprite=sequences[currentAnim];
            currentAnim++;
            if(currentAnim>=sequences.Count) 
                HasEnded=true;    

            
            return currentSprite;
        }

        public void Start(){
            isPlaying=true;
            HasEnded=false;
        }

        public void Reset(){
            currentAnim=0;
            isPlaying=false;
            HasEnded=false;
        }

        public float GetTime(){
            return speed*sequences.Count;
        }
    }

    public MacroAnimation MacroHit;
    public Sprite IdleSprite;
    public float timeBetweenHit;

    public List<MacroAnimation> macroAnimations;

    private SpriteRenderer spriteRenderer; 

    private int step=0;

    private HitManager hitManager;
    void Start()
    {
        hitManager=HitManager.SharedInstance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private float timeFromStart=0;
    void Update()
    {
        if(isAttacking) return;

        if(Input.GetAxis("Vertical1")<0){    
           timeFromStart+=Time.deltaTime;
           ChangeAnimation();
        }else if(Input.GetAxis("Vertical1")==0){
            if(timeFromStart>=macroAnimations[0].GetTime()){
                Debug.Log("STOP ALL");
                StopAllCoroutines();
                StartCoroutine(HitAndReset());
            }
        }else{
            Reset();
        }      
    }

    private void ChangeAnimation(){
        if(step>=macroAnimations.Count) 
            return;

        if(!macroAnimations[step].isPlaying){
            StartCoroutine(PlayMacroAnimation(macroAnimations[step]));
        }
           
    }
  
    private IEnumerator PlayMacroAnimation(MacroAnimation macroAnimation){
        macroAnimation.Reset();
        macroAnimation.Start();
        float macroAnimationTime=0;

        do{
            macroAnimationTime+=Time.deltaTime;
            
            if(macroAnimationTime>=macroAnimation.speed){
                spriteRenderer.sprite=macroAnimation.GetCurrentSequence();
                macroAnimationTime-=macroAnimation.speed;
            }

            yield return null;
        }while(!macroAnimation.HasEnded);

        step++;
        macroAnimation.isPlaying=false;
    }

    bool isAttacking=false;
    private IEnumerator HitAndReset(){
        isAttacking=true;
       
        MacroHit.Reset();
        MacroHit.Start();
        float macroAnimationTime=0;
        int counter=0;
        do{
            macroAnimationTime+=Time.deltaTime;
            
            if(macroAnimationTime>=MacroHit.speed){
                if(counter==0) hitManager.OnHit();
                spriteRenderer.sprite=MacroHit.GetCurrentSequence();
                macroAnimationTime-=MacroHit.speed;

                counter++;
            }

            yield return null;
        }while(!MacroHit.HasEnded);
        MacroHit.isPlaying=false;

        yield return new WaitForSeconds(timeBetweenHit);  
       
        isAttacking=false;
        Reset();
    }

    private void Reset(){
        Debug.Log("RESET");
        timeFromStart=0;
        step=0;
        spriteRenderer.sprite=IdleSprite;
    } 
}
