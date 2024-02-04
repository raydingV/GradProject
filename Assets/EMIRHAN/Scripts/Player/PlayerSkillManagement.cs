using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    void Start()
    {
        attackManager = GetComponent<PlayerAttackManager>();
    }

    private void Update()
    {
        InputKeyElements();
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
        GameObject newObject = GameObject.Instantiate(fireElement, gameObject.transform);
        newObject.transform.localScale = new Vector3(60,60,60);
    }

    public void FrozenElement()
    {
        attackManager.MagicObject = frozenAttack;
        attackManager.HoldEffect = frozenElementHold;
        GameObject newObject = GameObject.Instantiate(frozenElement, gameObject.transform);
        newObject.transform.localScale = new Vector3(60,60,60);
    }

    public void WindElement()
    {
        attackManager.MagicObject = windAttack;
        attackManager.HoldEffect = windElementHold;
        GameObject newObject = GameObject.Instantiate(windElement, gameObject.transform);
        newObject.transform.localScale = new Vector3(60,60,60);
    }
}
