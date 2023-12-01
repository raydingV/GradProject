using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    MagicAttack valuesOfMagic;

    [Header("ObjectVariable")]
    [SerializeField] GameObject MagicObject;
    GameObject newMagicObject;

    [Header("MechanicVariable")]
    float HoldValue;

    void Start()
    {
        HoldValue = 3f;
    }

    void Update()
    {
        HoldMouse();
        ReleaseMouse();
    }

    void HoldMouse()
    {
        if(Input.GetMouseButton(0))
        {
            HoldValue -= Time.deltaTime;

            Debug.Log(HoldValue);
        }   

    }

    void ReleaseMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            InitializeMagic();
        } 
    }

    void ValueOfMagic()
    {
        if (HoldValue < 3f)
        {
            valuesOfMagic.Speed = 40f;
        }
        else
        {
            valuesOfMagic.Speed = 5f;
        }

        HoldValue = 5f;

    }

    void InitializeMagic()
    {
        newMagicObject = GameObject.Instantiate(MagicObject);

        valuesOfMagic = newMagicObject.GetComponent<MagicAttack>();

        ValueOfMagic();

        newMagicObject.transform.rotation = gameObject.transform.rotation;

        newMagicObject.transform.position = gameObject.transform.position;

        Debug.Log("Created with " + valuesOfMagic.Speed + " Speed");
    }
}
