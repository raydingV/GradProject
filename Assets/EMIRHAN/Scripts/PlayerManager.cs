using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool InputEnable = true;

    void Start()
    {
  
    }

    void Update()
    {

    }

    public void PlayerDead()
    {
        StartCoroutine(GetCheckPoint());
    }

    private IEnumerator GetCheckPoint()
    {
        InputEnable = false;
        transform.position = new Vector3(0, 1, 0);
        yield return new WaitForSeconds(0.01f);
        InputEnable = true;
    }
}
