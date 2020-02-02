using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CustomersQueue CustomersQueue;

    void Start()
    {
        CustomersQueue.FillCustomers();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
