using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSource : MonoBehaviour
{

    //-- VARIABLES ---------------------------------------------------

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimations(string animation)
    {
        animator.SetBool("Moving", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Dashing", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("GettingDamage", false);

        animator.SetBool(animation, true);

    }
}
