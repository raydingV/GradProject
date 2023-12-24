using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainTarget : MonoBehaviour
{
    [SerializeField] RainAttack rain;
    void Start()
    {
        
    }

    void Update()
    {
        if(rain.needDestroy == true)
        {
            Destroy(gameObject);
        }
    }
}
