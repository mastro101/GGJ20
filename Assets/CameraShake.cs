using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;

    public int frequency;
    public float duration;
    public float AmplitudeAmount;

    private bool shakeX=false;
    private bool shakeY=false;

    float[] samples;
    float time;
    
    float k;
    bool isShaking;
    int sampleCount;
    private void Config(float duration,int frequency){
        this.frequency=frequency;
        this.duration=duration;
        if(duration<1)
            sampleCount=1*frequency;
        else
            sampleCount=(int)duration*frequency;
        samples= new float[sampleCount+1];
        for(int i=0;i<sampleCount;i++){
            samples[i]= (float)Random.value * 2 - 1;
        }
        isShaking=false;
    }

    public void Start(){
        time=0;
        isShaking=true;
        shakeX=false;
        shakeY=false;
    }

    public float Amplitude(float delta){
        time+=delta;
        if(time<duration){
        float s=time*frequency;
        int s0=(int)Mathf.Floor(s);
        int s1= s0+1;
            k=(duration-time)/duration;

            return (samples[s0]+(s-s0)*(samples[s1]-samples[s0]))*k;

        } else {
            isShaking = false;
            return 0;
        }
    }

    public void StartXShake(){
        Config(duration,frequency);
        Start();
        shakeX=true;
        StartCoroutine(Shake());
    }

    public void StartYShake(){
        Config(duration,frequency);
        Start();
        shakeY=true;
        StartCoroutine(Shake());
    }

    public IEnumerator Shake(){
        var startingPos=new Vector3(cameraTransform.position.x,cameraTransform.position.y,cameraTransform.position.z);
        float deltaAmount=0;
        while(isShaking){
            deltaAmount=Amplitude(Time.deltaTime)*AmplitudeAmount;

            if(shakeX)
                cameraTransform.position=startingPos+new Vector3(deltaAmount,0,0);
            else if(shakeY)
                cameraTransform.position=startingPos+new Vector3(0,deltaAmount,0);

            yield return null;
        }
        cameraTransform.position=startingPos;
        
    }

}
