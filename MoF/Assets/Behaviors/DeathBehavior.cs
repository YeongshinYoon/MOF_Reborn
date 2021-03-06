﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : StateMachineBehaviour {
    private float timePassed;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.transform.GetChild(0).gameObject);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timePassed += Time.deltaTime;

        EdgeCollider2D movableBound = FindObjectOfType<Bounds>().MyMovableBound;
        Vector3 respawnPos = new Vector3(Random.Range(movableBound.bounds.min.x, movableBound.bounds.max.x), Random.Range(movableBound.bounds.min.y, movableBound.bounds.max.y), animator.transform.position.z);
        if (timePassed >= 5f)
        {
            Instantiate(GameManager.MyInstance.MyMonsters[(int)animator.GetFloat("MonsterNumber")].gameObject, respawnPos, GameManager.MyInstance.MyMonsters[(int)animator.GetFloat("MonsterNumber")].gameObject.transform.rotation);
            animator.GetComponent<Enemy>().OnCharacterRemoved();
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    
    //}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
