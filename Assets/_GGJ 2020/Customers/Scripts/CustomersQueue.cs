using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersQueue : MonoBehaviour
{
    [SerializeField] Transform CustomerBox;
    [SerializeField] int DifferentCustomers;
    [SerializeField] Transform swordSpawnPoint;

    List<Customers> customers = new List<Customers>();

    public System.Action<Customers> onAddCustomers;

    int oldCustomerDifficult = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddCustomer();
            SpawnSword();
        }
    }

    public void AddCustomer()
    {
        Customers _customer = null;
        int randomInt = 1;
        for (int i = 0; i < 10; i++)
        {
            randomInt = Random.Range(1, DifferentCustomers + 1);
            _customer = PoolManager.SharedInstance.GetPooledObjectWithoutInstantiate("Customer" + randomInt).GetComponent<Customers>();
            if (oldCustomerDifficult == _customer.difficult)
            {
                continue;
            }
            else
            {
                _customer = PoolManager.SharedInstance.GetPooledObject("Customer" + randomInt).GetComponent<Customers>();
                break;
            }

        }
        _customer.transform.SetParent(CustomerBox);
        onAddCustomers?.Invoke(_customer.GetComponent<Customers>());
        oldCustomerDifficult = randomInt;
        customers.Add(_customer);
    }

    public Customers GetCustomers()
    {
        return customers[0];
    }

    public SwordMovement SpawnSword()
    {
        return customers[0].InstantiateSword(swordSpawnPoint.position);
    }

    public Customers NextCustomer()
    {
        GameObject _customer = customers[0].gameObject;
        customers.Remove(customers[0]);
        Destroy(_customer);
        return customers[0];
    }
}