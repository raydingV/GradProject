using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [Header("Values")]
    public float Speed = 20f;

    public GameObject BoomEffect;
    GameObject BoomObject;

    Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(transform.forward * Speed);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boss")
        {
            BoomObject = GameObject.Instantiate(BoomEffect);
            BoomObject.transform.position = gameObject.transform.position;
            BoomObject.transform.Rotate(-90, 0, 0);
            BoomObject.transform.localScale = new Vector3(5, 5, 5);

            Destroy(gameObject);
        }
    }
}
