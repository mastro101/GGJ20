using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersQueue : MonoBehaviour
{
    [SerializeField] Transform weaponSpawnPoint;
    public CustomerIconsUI customerIconsUI;
    
    private List<Customer> customers = new List<Customer>();

    Customer oldCustomer = null;
    public int numCustomerTypes;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextCustomer();
    }

    public Customer AddCustomer()
    {
        Customer customer = null;
        int randomInt = 1;
        int l = 10;
        for (int i = 0; i < l; i++)
        {
            randomInt = Random.Range(1, numCustomerTypes + 1);
            customer = PoolManager.SharedInstance.GetPooledObjectWithoutInstantiate("Customer" + randomInt).GetComponent<Customer>();
            if (oldCustomer != null)
            {
                if (oldCustomer.GetDifficulty() == customer.GetDifficulty())
                {
                    if (i == l - 1)
                        customer = PoolManager.SharedInstance.GetPooledObject("Customer1").GetComponent<Customer>();
                    continue;
                }
                else
                {
                    customer = PoolManager.SharedInstance.GetPooledObject("Customer" + randomInt).GetComponent<Customer>();
                    break;
                }
            }
        }

        customer.Activate(weaponSpawnPoint.position);

        oldCustomer = customer;
        customers.Add(customer);
        customerIconsUI.AddCustomer(customer);

        return customer;
    }

    public void NextCustomer()
    {
        AddCustomer();

        if(customers.Count>1){
            Customer lastCustomer = customers[0];
            lastCustomer.Deactivate();
            customers.Remove(lastCustomer);
         
            customerIconsUI.RemoveFirst();
        }
    }
}