using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerIcon : MonoBehaviour
{
    public Sprite green;
    public Sprite yellow;
    public Sprite orange;
    public Sprite red;

    private Image image;

    void Start(){
        image=GetComponent<Image>();
    }

    public void SetBaseImage(int difficulty){
        switch(difficulty){
            case 1:
                image.sprite=green;
                break;
            case 2:
                image.sprite=yellow;
                break;
            case 3:
                image.sprite=orange;
                break;
            case 4:
                image.sprite=red;
                break;   
        }
    }
}
