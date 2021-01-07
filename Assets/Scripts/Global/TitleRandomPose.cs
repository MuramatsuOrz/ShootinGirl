using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRandomPose : StateMachineBehaviour
{
    int random = Animator.StringToHash("random");

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash) { 
        animator.SetInteger(random, Random.Range(0, 4));
    }
}
