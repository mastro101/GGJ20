using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CustomerIconsUI : MonoBehaviour
{	
	List<GameObject> customersUI;
	Queue<GameObject> activeCustomersUI;
	private int availableSpots;

    void Start()
    {
		activeCustomersUI=new Queue<GameObject>();
		customersUI=new List<GameObject>();
		foreach (Transform child in transform){
			customersUI.Add(child.gameObject);
			child.gameObject.SetActive(false);	
		}		

		UpdateAvailableSpots();				
    }
	
	private void UpdateAvailableSpots(){
		availableSpots=customersUI.Where(c=>c.activeSelf==false).Count();
	}

	private int GetActiveCount(){
		return customersUI.Where(c=>c.activeSelf==true).Count();
	}

	private bool AreSpotsAvailable(){
		return availableSpots>0;
	}

	public bool SetCustomerIcon(Customer customer){
		if(!AreSpotsAvailable())
			return false;

		var customerUI=customersUI.Where(c=>c.activeSelf==false).FirstOrDefault();		
		var customerIcon=customerUI.GetComponent<CustomerIcon>();
		customerIcon.ConfigIcon(customer);
		customerUI.gameObject.SetActive(true);
		activeCustomersUI.Enqueue(customerUI);

		UpdateIcons();
		UpdateAvailableSpots();		
		return true;
	}
	private void UpdateIcons(){
		int children = transform.childCount;
		bool isBannerSet=false;
        for (int i = 0; i < children; ++i){
			var child=transform.GetChild(i).gameObject;
			if(child.activeSelf){
				if(!isBannerSet){
					child.GetComponent<CustomerIcon>().SetToTopBanner();
					isBannerSet=true;
				}else{
					child.GetComponent<CustomerIcon>().SetToNormalBanner();
				}				
			}
		}

	}
	public void RemoveLastAndReorder(){
		var firstToServe=activeCustomersUI.Dequeue();
		firstToServe.gameObject.SetActive(false);
		
		UpdateIcons();
		UpdateAvailableSpots();		
	}
}
