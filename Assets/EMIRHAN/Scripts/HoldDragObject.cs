using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDragObject : MonoBehaviour
{
    [SerializeField] public int ObjectTag;
    [SerializeField] PlayerManager player;
    [SerializeField] EnteranceLevelManager levelManager;

    Rigidbody rb;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<EnteranceLevelManager>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HoldObject();
        RelaseObject();
    }

    void HoldObject()
    {
        if (Input.GetKeyDown(KeyCode.E) && player != null && levelManager.puzzleDone == false)
        {
            transform.position = player.holdObject.transform.position;
            gameObject.tag = "Untagged";
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.parent = player.transform;
        }
    }

    void RelaseObject()
    {
        if (Input.GetKeyDown(KeyCode.G) && player != null)
        {
            gameObject.tag = "HoldObject";
            rb.freezeRotation = false;
            //rb.constraints = RigidbodyConstraints.None;
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player = null;
        }
    }
}
