using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_endMeleeAttack : StateMachineBehaviour {

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        PlayerController player = animator.GetComponent<PlayerController>();

        player.isMeleeAttack = false;
	}
}
