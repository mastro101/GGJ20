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

    public EffectManager hitEffect;

    public MacroAnimation MacroHit;
    public Sprite IdleSprite;
    public float timeBetweenHit;

    public List<MacroAnimation> macroAnimations;

    private SpriteRenderer spriteRenderer; 

    public int step=0;

    private HitManager hitManager;
	
	private SoundManager soundManager;
    
    void Start()
    {
        hitManager=HitManager.SharedInstance;
        spriteRenderer = GetComponent<SpriteRenderer>();
		soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private float timeFromStart=0;
    private bool sequenceStarted=true;
    void Update()
    {
        if(isAttacking) 
        return;

        if(Mathf.Floor(Input.GetAxis("Vertical1"))==-1 || Input.GetKey(KeyCode.Return)){    
            if(!sequenceStarted)
                sequenceStarted=true;           
            timeFromStart+=Time.deltaTime;
            ChangeAnimation();
        }else if(sequenceStarted){
            StopAllCoroutines();
            if(timeFromStart>=macroAnimations[0].GetTime())                     
                StartCoroutine(HitAndReset());
            else          
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
		
		switch (step) {
			case 0:
				soundManager.Play("InizioCarica");
				break;
			case 3:
				soundManager.Play("Carica");
				break;
		};
		
        while(true){
            macroAnimationTime+=Time.deltaTime;
            
            if(macroAnimationTime>=macroAnimation.speed){
                spriteRenderer.sprite=macroAnimation.GetCurrentSequence();
                macroAnimationTime-=macroAnimation.speed;
            }else if(macroAnimationTime>=macroAnimation.GetTime())
                break;

            if(macroAnimation.HasEnded)
                break;
            else
                yield return null;
        }

        step++;
        macroAnimation.isPlaying=false;
    }

    bool isAttacking=false;
    private IEnumerator HitAndReset(){
        isAttacking=true;

        
        yield return PlayHitAnimation();
        yield return new WaitForSeconds(timeBetweenHit);  
        
        isAttacking=false;
        Reset();
    }


    private IEnumerator PlayHitAnimation(){
        MacroHit.Reset();
        MacroHit.Start();
        float macroAnimationTime=0;
        int counter=0;
        do{
            macroAnimationTime+=Time.deltaTime;
            
            if(macroAnimationTime>=MacroHit.speed){
                if(counter==0) hitManager.OnHit();
                if(counter==1) hitEffect.Play();
                spriteRenderer.sprite=MacroHit.GetCurrentSequence();
                macroAnimationTime-=MacroHit.speed;

                counter++;
            }

            yield return null;
        }while(!MacroHit.HasEnded);
        MacroHit.isPlaying=false;
    }
    private void Reset(){
        timeFromStart=0;
        step=0;
        sequenceStarted=false;
        spriteRenderer.sprite=IdleSprite;

        foreach(var macroAnimation in macroAnimations){
            macroAnimation.Reset();
        }
    } 
}
