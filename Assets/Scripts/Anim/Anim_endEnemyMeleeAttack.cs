﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_endEnemyMeleeAttack : StateMachineBehaviour {



	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<Enemy_Behaviour>().EndMeleeAttack();
	}
}
