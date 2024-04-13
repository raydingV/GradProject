using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillManagement : MonoBehaviour
{
    [Header("Scripts")]
    PlayerAttackManager attackManager;
    PlayerManager playerManager;

    [Header("AttackObjects")]
    [SerializeField] GameObject fireAttack;
    [SerializeField] GameObject frozenAttack;
    [SerializeField] GameObject windAttack;

    [Header("ElementVFX")]
    [SerializeField] GameObject fireElement;
    [SerializeField] GameObject frozenElement;
    [SerializeField] GameObject windElement;

    [Header("HoldVFX")]
    [SerializeField] ParticleSystem fireElementHold;
    [SerializeField] ParticleSystem frozenElementHold;
    [SerializeField] ParticleSystem windElementHold;

    [Header("SkillUI")] 
    [SerializeField] private GameObject[] skillUIEnable;
    [SerializeField] private GameObject[] skillUIDisable;

    [SerializeField] private Transform transformFire;

    void Awake()
    {
        attackManager = GetComponent<PlayerAttackManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        InputKeyElements();
    }

    private void ElementSpawn(GameObject _newObject)
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hit;
        
        if (groundPlane.Raycast(ray, out hit))
        {
            Vector3 pointToLook = ray.GetPoint(hit);

            Vector3 lookDirection = new Vector3(pointToLook.x - transform.position.x, 0f, pointToLook.z - transform.position.z);
            Quaternion look = Quaternion.LookRotation(lookDirection);
            GameObject newObject = Instantiate(_newObject, gameObject.transform.position, look);
        }
    }

    private void InputKeyElements()
    {
        switch(Input.inputString)
        {
            case "1":
                FireElement();
                DisableUI(0);
                break;
            case "2":
                FrozenElement();
                DisableUI(1);
                break;
            case "3":
                WindElement();
                DisableUI(2);
                break;
        }
    }

    private void DisableUI(int value)
    {
        for (int i = 0; i < skillUIEnable.Length; i++)
        {
            if (value != i)
            {
                skillUIEnable[i].SetActive(false);
                skillUIDisable[i].SetActive(true);
            }
            else
            {
                skillUIEnable[i].SetActive(true);
                skillUIDisable[i].SetActive(false);
            }
        }

    }

    public void FireElement()
    {
        attackManager.MagicObject = fireAttack;
        attackManager.HoldEffect = fireElementHold;
        // GameObject newObject = Instantiate(fireElement, transformFire.position, transformFire.rotation);
        ElementSpawn(fireElement);
        //newObject.transform.localScale = new Vector3(60,60,60);
    }

    public void FrozenElement()
    {
        attackManager.MagicObject = frozenAttack;
        attackManager.HoldEffect = frozenElementHold;
        // GameObject newObject = Instantiate(frozenElement, transformFire.position, transformFire.rotation);
        ElementSpawn(frozenElement);
        // newObject.transform.localScale = new Vector3(60,60,60);
    }

    public void WindElement()
    {
        attackManager.MagicObject = windAttack;
        attackManager.HoldEffect = windElementHold;
        // GameObject newObject = Instantiate(windElement, transformFire.position, transformFire.rotation);
        // // newObject.transform.localScale = new Vector3(60,60,60);
        StartCoroutine(windVFX());
    }

    IEnumerator windVFX()
    {
        windElement.SetActive(true);
        playerManager.canTrigger = false;
        yield return new WaitForSeconds(5f);
        windElement.SetActive(false);
        playerManager.canTrigger = true;
    }
}
