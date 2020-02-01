using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_System : MonoBehaviour
{
	
	public Image[] customersUI;
	public int nCustomers = 0;
	public Sprite placeHolder;
	
    void Start()
    {
        customersUI = new Image[6];
		for(int i=0; i<6; i++) {
			customersUI[i] = GameObject.Find("customer"+i.ToString()).GetComponent<Image>();
		}
    }

    void FixedUpdate()
    {
        int newNCustomers=GameObject.Find("Customers").GetComponent<CustomersQueue>().customers.Count;
		if(newNCustomers!=nCustomers) {
			for(int j=0; j<6; j++) {
				if(j>=(newNCustomers)) {
					customersUI[j] = null;
				}
				else {
					//customersUI.sprite = GameObject.Find("Customers").GetComponent<CustomersQueue>().customers[j].customerIcon;
					customersUI[j].sprite = placeHolder;
				}
			}
		}
		nCustomers=newNCustomers;
    }
}
