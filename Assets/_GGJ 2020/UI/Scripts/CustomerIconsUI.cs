using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CustomerIconsUI : MonoBehaviour
{	
	List<GameObject> customersUI;
	private int availableSpots;

    void Start()
    {
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

	private bool AreSpotsAvailable(){
		return availableSpots>0;
	}

	public bool AddCustomer(Customer customer){
		if(!AreSpotsAvailable())
			return false;

		Debug.Log("Difficulty: "+customer.GetDifficulty());
		var customerUI=customersUI.Where(c=>c.activeSelf==false).FirstOrDefault();		
		var customerIcon=customerUI.GetComponent<CustomerIcon>();
		customerIcon.SetBaseImage(customer.GetDifficulty());
		customerUI.gameObject.SetActive(true);
		UpdateAvailableSpots();		
		return true;
	}

	public void RemoveFirst(){
		var firstCustomerUI=customersUI.First();
		firstCustomerUI.gameObject.SetActive(true);
	}
}
