using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePuzzle : MonoBehaviour
{
    [SerializeField] EnteranceLevelManager levelManager;
    [SerializeField] GameObject _flyObject;
    [SerializeField] HoldDragObject _holdObject;
    [SerializeField] int tagHolder;

    bool checkOnce = false;

    Rigidbody rb;

    Vector3 startTransform;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<EnteranceLevelManager>();
        startTransform = transform.position;
    }

    void Update()
    {
        flyToObject();
    }

    void flyToObject()
    {
        float yOffset = Mathf.Sin(Time.time * 1) * 0.3f;
        transform.position = startTransform + new Vector3(0f, yOffset, 0f);
    }

    void controlTag(int tag)
    {
        if(tag == tagHolder && checkOnce == false)
        {
            levelManager.puzzleControl[tag] = true;
            levelManager.checkPuzzle();
            checkOnce = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "HoldObject")
        {
            //rb = other.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeAll;

            _holdObject = other.GetComponent<HoldDragObject>();
            controlTag(_holdObject.ObjectTag);

            other.transform.position = _flyObject.transform.position;
            other.transform.rotation = _flyObject.transform.rotation;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Untagged")
        {
            rb = null;
            _holdObject = null;
            levelManager.puzzleControl[tagHolder] = false;
            checkOnce = false;
        }
    }
}
