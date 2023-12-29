using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] SkillsData skillData;

    void Start()
    {
        GetSkillData();
        animator = GetComponent<Animator>();
    }

    public void intParamater(string name, int parameter)
    {
        animator.SetInteger(name, parameter);
    }

    public void floatParameter(string name, float parameter)
    {
        animator.SetFloat(name, parameter);
    }

    public void boolParameter(string name, bool parameter)
    {
        animator.SetBool(name, parameter);
    }

    void GetSkillData()
    {
        skillData._bossAnimation = this;
    }
}
