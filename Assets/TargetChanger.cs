using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChanger : MonoBehaviour
{
    public List<Sprite> sequences;
    // Start is called before the first frame update
    public void SetRandomTarget(){
        var SpriteRenderer=GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite=sequences[Random.Range(0,sequences.Count+1)];
    }

}
