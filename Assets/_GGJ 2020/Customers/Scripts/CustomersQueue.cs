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
	
    public System.Action<int> onQueueChange;

	private SoundManager soundManager;

    private void Start() {
		soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
	
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
        if (customers.Count >= maxCustomers)
        {
            onQueueChange?.Invoke(customers.Count);
        }

		var randomInt = fixedCustomerToSpawn!=0? fixedCustomerToSpawn:RandomRangeExcept(1,numCustomerTypes+1,prevType);

        var customer = PoolManager.SharedInstance.GetPooledObject("Customer" + randomInt).GetComponent<Customer>();
        customer.Activate();
        prevType=randomInt;

		customers.Add(customer);
		customerIconsUI.SetCustomerIcon(customer);
		
		/*int n = Random.Range(0,2);
		switch(n){
			case 0:*/
			soundManager.Play("ArrivoCliente1");
			/*break;
			case 1:
			soundManager.Play("ArrivoCliente2");
			break;
			case 2:
			soundManager.Play("ArrivoCliente3");
			break;
		};*/
		
    }
	
	public IEnumerator FillQueue(){
        while(customers.Count<maxCustomers + 1){
            AddCustomer();
            yield return new WaitForSeconds(delayBetweenCustomers);
        }
    }

    public void FillCustomers(){
        StartCoroutine(FillQueue());
    }

    public void RemoveLast(){
        var lastCustomer=customers.First();
        customers.Remove(lastCustomer);
        lastCustomer.Deactivate();
        customerIconsUI.RemoveLastAndReorder();
		
		soundManager.Play("Vittoria1");
		
    }

    public Customer ServeCustomer(){
        var customer=customers.First();
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