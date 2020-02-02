using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomersQueue : MonoBehaviour
{
    [SerializeField] Transform weaponSpawnPoint;
    public CustomerIconsUI customerIconsUI;

    public int fixedCustomerToSpawn; 
    public int maxCustomers;
    public float delayBetweenCustomers;
    private List<Customer> customers = new List<Customer>();

    Customer oldCustomer = null;
    public int numCustomerTypes;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		    AddCustomer();

        if (Input.GetKeyDown(KeyCode.C))
            RemoveLast();
    }

    int prevType=-1;
	public void AddCustomer()
    {
		var randomInt = fixedCustomerToSpawn!=0? fixedCustomerToSpawn:RandomRangeExcept(1,2,prevType);

        var customer = PoolManager.SharedInstance.GetPooledObject("Customer" + randomInt).GetComponent<Customer>();
        customer.Activate();
        prevType=customer.GetDifficulty();

		customers.Add(customer);
		customerIconsUI.SetCustomerIcon(customer);
    }
	
	public IEnumerator FillQueue(){
        while(customers.Count<maxCustomers){
            AddCustomer();
            yield return new WaitForSeconds(delayBetweenCustomers);
        }
    }

    public void FillCustomers(){
        StartCoroutine(FillQueue());
    }

    public void RemoveLast(){
        var lastCustomer=customers.Last();
        customers.Remove(lastCustomer);
        lastCustomer.Deactivate();
        customerIconsUI.RemoveLastAndReorder();
    }

    public Customer ServeCustomer(){
        var customer=customers.Last();
        customer.ActivateAll(weaponSpawnPoint.position);
        return customer;
    }

    private int RandomRangeExcept(int min, int max, int except=-1)  {
        var exclude = new HashSet<int>() { except };
        var range = Enumerable.Range(min, max).Where(i => !exclude.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(0, (max-1) - exclude.Count);
        return range.ElementAt(index);
    }
} 