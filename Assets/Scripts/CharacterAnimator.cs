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
        float speedPercent;
        float navMeshAgentVelocity = navMeshAgent.velocity.magnitude;
        if (navMeshAgentVelocity < .1f) {
            speedPercent = 0;
        }
        else {
            speedPercent = navMeshAgentVelocity / navMeshAgent.speed;
        }

        //animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("speedPercent", speedPercent);
    }
}
