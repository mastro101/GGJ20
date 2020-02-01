using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersQueue : MonoBehaviour
{
    [SerializeField] Transform weaponSpawnPoint;
    public CustomerIconsUI customerIconsUI;
    
    [SerializeField]
    [Tooltip("è moltiplicato per la difficoltà della spada")] 
    float timeForNextCustomer = 1;

    public List<Customer> customers = new List<Customer>();
    [HideInInspector] public float timer;

    Customer oldCustomer = null;
    bool first;

    private void Awake()
    {
        first = true;
    }

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
            randomInt = Random.Range(1, customers.Count + 1);
            customer = PoolManager.SharedInstance.GetPooledObjectWithoutInstantiate("Customer" + randomInt).GetComponent<Customer>();
            if (oldCustomer != null)
            {
                if (oldCustomer.difficult == customer.difficult)
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

        customer.gameObject.SetActive(true);
        SpawnSword(customer);
        customer.currentSword.gameObject.SetActive(true);
        oldCustomer = customer;
        customers.Add(customer);
  
        if (corutineNextCustomer != null)
            StopCoroutine(corutineNextCustomer);
        corutineNextCustomer = CorutineNextCustomer();
        StartCoroutine(corutineNextCustomer);
        return customer;
    }

    public void SpawnSword(Customer customer)
    {
        customer.InstantiateSword(weaponSpawnPoint.position);
        customer.currentSword.gameObject.SetActive(false);
    }

    public Customer NextCustomer()
    {
        if(customers.Count == 0 || customers.Count == 1)
            AddCustomer();

        if (first)
        {
            first = false;
            return customers[0];
        }
        Customer lastCustomer = customers[0];
        lastCustomer.currentSword.gameObject.SetActive(false);
        customers.Remove(customers[0]);
        lastCustomer.transform.SetParent(null);
        lastCustomer.gameObject.SetActive(false);
        return customers[0];
    }

    IEnumerator corutineNextCustomer;
    IEnumerator CorutineNextCustomer()
    {
        timer = timeForNextCustomer * oldCustomer.difficult;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        AddCustomer();
    }
}