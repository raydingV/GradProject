using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAttackManager : MonoBehaviour
{
    MagicAttack valuesOfMagic;

    [Header("Component")]
    [SerializeField] GameObject MagicObject;
    GameObject newMagicObject;

    [Header("MechanicVariable")]
    float HoldValue;

    [Header("VfxMaterial")]
    public GameObject HoldEffect;
    GameObject effectObject;

    bool oneInstantiate = false;

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

            if(oneInstantiate == false)
            {
                effectObject = GameObject.Instantiate(HoldEffect);
                oneInstantiate = true;
            }

            effectObject.transform.position = gameObject.transform.position;
            //Debug.Log(HoldValue);
        }
    }

    void ReleaseMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            InitializeMagic();
            Destroy(effectObject);
            oneInstantiate = false;
        } 
    }

    void ValueOfMagic()
    {
        if (HoldValue < 0f)
        {
            valuesOfMagic.Speed = 80f;
        }
        else
        {
            valuesOfMagic.Speed = 40f;
        }

        HoldValue = 1f;

    }

    void InitializeMagic()
    {
        newMagicObject = GameObject.Instantiate(MagicObject);

        valuesOfMagic = newMagicObject.GetComponent<MagicAttack>();

        ValueOfMagic();

        newMagicObject.transform.rotation = gameObject.transform.rotation;

        newMagicObject.transform.position = gameObject.transform.position;

        //Debug.Log("Created with " + valuesOfMagic.Speed + " Speed");
    }
}
