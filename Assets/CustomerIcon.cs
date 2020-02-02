using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerIcon : MonoBehaviour
{
    public Sprite greenTopBanner;
    public Sprite yellowTopBanner;
    public Sprite orangeTopBanner;
    public Sprite redTopBanner;

    public Sprite green;
    public Sprite yellow;
    public Sprite orange;
    public Sprite red;

    private Image image;
    private Customer customer;
    void Start(){
        image=GetComponent<Image>();
    }

    public void ConfigIcon(Customer customer){
        this.customer=customer;
    }

    public void SetToNormalBanner(){
        GetComponent<RectTransform>().sizeDelta=new Vector2(164,250);
        SetBaseImage(customer.GetDifficulty(),false);
    }

    public void SetToTopBanner(){
        GetComponent<RectTransform>().sizeDelta=new Vector2(250,250);
        SetBaseImage(customer.GetDifficulty(),true);
    }

    private void SetBaseImage(int difficulty,bool useTopBanner){
        switch(difficulty){
            case 1:
                image.sprite=useTopBanner?greenTopBanner:green;
                break;
            case 2:
                image.sprite=useTopBanner?yellowTopBanner:yellow;
                break;
            case 3:
                image.sprite=useTopBanner?orangeTopBanner:orange;
                break;
            case 4:
                image.sprite=useTopBanner?redTopBanner:red;
                break;   
        }
    }
}