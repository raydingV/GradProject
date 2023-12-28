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
    [SerializeField] float Speed;

    [Header("VfxMaterial")]
    public ParticleSystem HoldEffect;
    public ParticleSystem HoldEffectChild;
    ParticleSystem effectObject;
    ParticleSystem effectObjectChild;
    [SerializeField] Transform magicAttackTransform;

    bool oneInstantiate = false;

    void Start()
    {
        HoldValue = 3f;
    }

    void LateUpdate()
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
                effectObject = Instantiate(HoldEffect);
                effectObjectChild = Instantiate(HoldEffectChild);
                effectObjectChild.loop = true;
                effectObject.loop = true;
                oneInstantiate = true;
                InitializeMagic();
            }

            if(newMagicObject != null)
            {
                FollowPlayer();
                BiggerScale();
            }

            effectObject.transform.position = gameObject.transform.position;
            effectObjectChild.transform.position = gameObject.transform.position;
            //Debug.Log(HoldValue);
        }
    }

    void ReleaseMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            valuesOfMagic.StartFunc = true;
            effectObject.loop = false;
            effectObjectChild.loop = false;
            valuesOfMagic.tag = "Magic";
            oneInstantiate = false;
        } 
    }

    void ValueOfMagic()
    {
        if (HoldValue < 0f)
        {
            valuesOfMagic.Speed = Speed;
        }
        else
        {
            valuesOfMagic.Speed = Speed;
        }

        HoldValue = 1f;
    }

    void InitializeMagic()
    {
        newMagicObject = GameObject.Instantiate(MagicObject);

        valuesOfMagic = newMagicObject.GetComponent<MagicAttack>();

        ValueOfMagic();

        FollowPlayer();

        //Debug.Log("Created with " + valuesOfMagic.Speed + " Speed");
    }

    void FollowPlayer()
    {
        newMagicObject.transform.rotation = gameObject.transform.rotation;

        newMagicObject.transform.position = new Vector3(magicAttackTransform.position.x, newMagicObject.transform.position.y, magicAttackTransform.transform.position.z);
    }

    void BiggerScale()
    {
        if(newMagicObject.transform.localScale.x <= 1f)
        {
            newMagicObject.transform.localScale = new Vector3(newMagicObject.transform.localScale.x + Time.deltaTime / 4, newMagicObject.transform.localScale.y + Time.deltaTime / 4, newMagicObject.transform.localScale.z + Time.deltaTime / 4);
        }
    }
}
