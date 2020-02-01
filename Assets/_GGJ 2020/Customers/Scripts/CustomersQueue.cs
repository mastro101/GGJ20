using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersQueue : MonoBehaviour
{
    [SerializeField] Transform CustomerBox;
    [SerializeField] int DifferentCustomers;
    [SerializeField] Transform swordSpawnPoint;
    [SerializeField][Tooltip("è moltiplicato per la difficoltà della spada")] float timeForNextCustomer = 1;

    public List<Customers> customers = new List<Customers>();
    [HideInInspector] public float timer;

    Customers oldCustomer = null;
    bool first;

    private void Awake()
    {
        first = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextCustomer();
            SpawnSword();
        }
    }

    public Customers AddCustomer()
    {
        Customers _customer = null;
        int randomInt = 1;
        int l = 10;
        for (int i = 0; i < l; i++)
        {
            randomInt = Random.Range(1, DifferentCustomers + 1);
            _customer = PoolManager.SharedInstance.GetPooledObjectWithoutInstantiate("Customer" + randomInt).GetComponent<Customers>();
            if (oldCustomer != null)
            {
                if (oldCustomer.difficult == _customer.difficult)
                {
                    if (i == l - 1)
                        _customer = PoolManager.SharedInstance.GetPooledObject("Customer1").GetComponent<Customers>();
                    continue;
                }
                else
                {
                    _customer = PoolManager.SharedInstance.GetPooledObject("Customer" + randomInt).GetComponent<Customers>();
                    break;
                }
            }
        }
        _customer.transform.SetParent(CustomerBox);
        _customer.gameObject.SetActive(true);
        oldCustomer = _customer;
        customers.Add(_customer);

        if (corutineNextCustomer != null)
            StopCoroutine(corutineNextCustomer);
        corutineNextCustomer = CorutineNextCustomer();
        StartCoroutine(corutineNextCustomer);
        return _customer;
    }

    public SwordMovement SpawnSword()
    {
        return customers[0].InstantiateSword(swordSpawnPoint.position);
    }

    public Customers NextCustomer()
    {
        if (customers.Count == 1)
        {
            AddCustomer();
        }
        else if(customers.Count == 0)
        {
            AddCustomer();
        }

        if (first)
        {
            first = false;
            return customers[0];
        }
        Customers _customer = customers[0];
        Destroy(_customer.currentSword.gameObject);
        customers.Remove(customers[0]);
        _customer.transform.SetParent(null);
        _customer.gameObject.SetActive(false);
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

    //IEnumerator corutineCheckNextCustomer;
    //IEnumerator CorutineCheckNextCustomer()
    //{
    //    while (customers.Count == 0)
    //    {
    //        yield return null;
    //    }
    //    NextCustomer();
    //}
}