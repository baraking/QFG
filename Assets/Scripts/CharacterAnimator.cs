using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    const float locomationAnimationSmoothTime = .1f;

    Animator animator;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("CastSpell") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
            HeroActionsManager.instance.isCasting = false;
            animator.SetBool("isCasting", HeroActionsManager.instance.isCasting);
        }
        if (HeroActionsManager.instance.isCasting)
        {
            animator.enabled = false;
            animator.enabled = true;
            animator.SetBool("isCasting", HeroActionsManager.instance.isCasting);
        }
        else
        {
            float speedPercent;
            float navMeshAgentVelocity = navMeshAgent.velocity.magnitude;
            if (navMeshAgentVelocity < .1f)
            {
                speedPercent = 0;
            }
            else
            {
                speedPercent = navMeshAgentVelocity / navMeshAgent.speed;
            }

            //animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
            animator.SetFloat("speedPercent", speedPercent);
        }
    }
}
