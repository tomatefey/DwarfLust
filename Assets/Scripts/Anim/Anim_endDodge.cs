using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_endDodge : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController player = animator.GetComponent<PlayerController>();

        player.isDodge = false;
    }
}
