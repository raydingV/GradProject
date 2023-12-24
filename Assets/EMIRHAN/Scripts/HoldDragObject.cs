using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDragObject : MonoBehaviour
{
    public GameObject player;

    bool CanHold = false;
    bool Holding = false;

    void Start()
    {
        
    }

    void Update()
    {
        DistanceCalculate();
        HoldObject();
        RelaseObject();
    }

    void DistanceCalculate()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 2f && Holding == false)
        {
            CanHold = true;
        }
        else
        {
            CanHold = false;
        }
    }

    void HoldObject()
    {
        if(Input.GetKeyDown(KeyCode.E) && CanHold == true)
        {
            gameObject.transform.parent = player.transform;
            Holding = true;
        }
    }

    void RelaseObject()
    {
        if (Input.GetKeyDown(KeyCode.G) && Holding == true)
        {
            gameObject.transform.parent = null;
            Holding = false;
        }
    }
}
