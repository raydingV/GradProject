using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] PlayerMovementManager playerMovementManager;
    [SerializeField] PlayerAttackManager playerAttackManager;
    [SerializeField] PlayerGlutonyPuzzle playerGlutonyPuzzle;

    float inputValue;
    float walkPuzzleValue;

    void Awake()
    {
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        animator = GetComponent<Animator>();    
        playerGlutonyPuzzle = GetComponent<PlayerGlutonyPuzzle>();
    }

    void Update()
    {
        Running();
        Dashing();
        Attacking();

        if(playerGlutonyPuzzle != null && playerGlutonyPuzzle.finishPuzzle == false)
        {
            PuzzleRunning();
        }
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

    void PuzzleRunning()
    {
        if(playerGlutonyPuzzle.clickedObject != null || playerGlutonyPuzzle.startTransform == true)
        {
            walkPuzzleValue = 0.2f;
        }
        else
        {
            walkPuzzleValue = 0;
        }

        animator.SetFloat("Speed", walkPuzzleValue);
    }
}
