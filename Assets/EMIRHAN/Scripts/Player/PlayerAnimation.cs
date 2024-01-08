using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] PlayerMovementManager playerMovementManager;
    [SerializeField] PlayerAttackManager playerAttackManager;

    float inputValue;

    void Awake()
    {
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        Running();
        Dashing();
        Attacking();
    }

    void Running()
    {
        inputValue = (Mathf.Abs(playerMovementManager.controlPlayer.x) + Mathf.Abs(playerMovementManager.controlPlayer.z));
        animator.SetFloat("Speed", inputValue);
    }

    void Dashing()
    {
        animator.SetBool("Dash", playerMovementManager.InDashing);
    }

    void Attacking()
    {
        animator.SetBool("RunFire", playerAttackManager.InAttack);
    }
}
