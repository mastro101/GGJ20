using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject Effect;
    public string AnimationName;
    private Animator anim;
    public bool IsPlaying { get; private set; }

    void OnEnable() {
         anim = Effect.GetComponent<Animator>();
    }

    public void Play(){
        if(IsPlaying) return;
        Effect.SetActive(true);          
        StartCoroutine(PlayUntilCompleted());
    }

    private IEnumerator PlayUntilCompleted()
    {
        IsPlaying = true;
            
        anim.Play(AnimationName);

        while (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            yield return null;

        Effect.SetActive(false);
        IsPlaying = false;
    }
}
