using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerIconsUI : MonoBehaviour
{
	
	Image[] customersUI;
	int nCustomers = 0;
	public Sprite placeHolder;
	
    void Start()
    {
        customersUI = new Image[5];
		for(int i=0; i<5; i++) {
			customersUI[i] = GameObject.Find("customer"+i.ToString()).GetComponent<Image>();
		}
    }

    void FixedUpdate()
    {
        int newNCustomers=GameObject.Find("GameManager").GetComponent<CustomersQueue>().customers.Count;
		if(newNCustomers!=nCustomers) {
			for(int j=0; j<5; j++) {
				if(j>=(newNCustomers)) {
					customersUI[j] = null;
				}
				else {
					//customersUI[j].sprite = GameObject.Find("Customers").GetComponent<CustomersQueue>().customers[j].customerIcon;
					customersUI[j].sprite = placeHolder;
				}
			}
		}
		nCustomers=newNCustomers;
    }
}
