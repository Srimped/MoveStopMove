using UnityEngine;

public class AnimationState : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    public Player player;
    public PlayerAlive playerAlive;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        bool isMoving = playerMovement.movement != Vector3.zero;
        bool isAttacking = player.inRange;
        bool isWinning = playerAlive.isWinning;

        if (isMoving)
        {
            if (!animator.GetBool("IsRunning"))
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            if (isAttacking)
            {
                if (!animator.GetBool("IsAttack"))
                {
                    animator.SetBool("IsAttack", true);
                    animator.SetBool("IsIdle", false);
                    animator.SetBool("IsRunning", false);
                }
            }
            else
            {
                if (!animator.GetBool("IsIdle"))
                {
                    animator.SetBool("IsIdle", true);
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsRunning", false);
                }
            }
        }

        if (player.isDead == true)
        {
            animator.SetBool("IsDead", true);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsRunning", false);
            playerMovement.enabled = false;
        }

        if (isWinning == true)
        {
            animator.SetBool("IsWin", true);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsRunning", false);
        }
    }

}
