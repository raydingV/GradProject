using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        attackManager = GetComponent<PlayerAttackManager>();
    }

    public void FireElement()
    {
        attackManager.MagicObject = fireAttack;
        attackManager.HoldEffect = fireElementHold;
        GameObject.Instantiate(fireElement, gameObject.transform);        
    }

    public void FrozenElement()
    {
        attackManager.MagicObject = frozenAttack;
        attackManager.HoldEffect = frozenElementHold;
        GameObject.Instantiate(frozenElement, gameObject.transform);
    }

    public void WindElement()
    {
        
        attackManager.MagicObject = windAttack;
        attackManager.HoldEffect = windElementHold;
        GameObject.Instantiate(windElement, gameObject.transform);
    }
}
