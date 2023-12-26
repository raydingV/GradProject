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

    float Timer = 6;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            Destroy(gameObject);
        }

        rb.AddForce(transform.forward * Speed);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2, gameObject.transform.position.z);
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * Speed);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2, gameObject.transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boss")
        {
            BoomObject = GameObject.Instantiate(BoomEffect);
            BoomObject.transform.position = gameObject.transform.position;
            BoomObject.transform.Rotate(-90, 0, 0);
            BoomObject.transform.localScale = new Vector3(2, 2, 2);

            Destroy(gameObject);
        }
    }
}
