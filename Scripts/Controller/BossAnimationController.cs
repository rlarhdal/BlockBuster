using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator animator;

    private int IsAttack = Animator.StringToHash("isAttack");
    private int IsHit = Animator.StringToHash("isHit");
    private int IsDead = Animator.StringToHash("isDead");
    private float magnituteThreshold = 0.5f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("OnAttack");
    }

    public void Hit()
    {
        animator.SetTrigger("OnHit");
    }


    public void Dead()
    {
        animator.SetBool (IsDead, true);
    }
}