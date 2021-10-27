using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator animator;
    public float attackTimer = 30f;
    private float currentAttackTimer = 0;

    private bool isAttackedThisFrame = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "MainCamera") return;
        //isAttackedThisFrame = true;
    }

    private void OnTriggerStay(Collider other)
    {
        currentAttackTimer += Time.deltaTime;
        if (currentAttackTimer > attackTimer && other.gameObject.tag == "MainCamera")
        {
            isAttackedThisFrame = true;
            currentAttackTimer = 0;
        }
    }

    private void Update()
    {
        if (isAttackedThisFrame)
        {
            animator.SetBool("attack", true);
            isAttackedThisFrame = false;
        }
    }
}
