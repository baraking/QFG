using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class CharacterAnimator : MonoBehaviour
{

    const float locomationAnimationSmoothTime = .1f;

    Animator animator;
    NavMeshAgent navMeshAgent;

    public Rig rightHandRig;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        rightHandRig.weight = 0;
    }

    public void SetRightHandAimTarget(Transform targetsTransform)
    {
        rightHandRig.GetComponent<TwoBoneIKConstraint>().data.target.position = targetsTransform.position;
    }

    public void ResetRightHandAimTarget()
    {
        rightHandRig.GetComponent<TwoBoneIKConstraint>().data.target.position = rightHandRig.GetComponent<TwoBoneIKConstraint>().data.tip.position;
    }

    private void Update()
    {
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
