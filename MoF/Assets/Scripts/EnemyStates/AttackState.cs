﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState {
    private Enemy parent;

    private float attackCooldown = 3;

    private float extraRange = 0.1f;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        
    }
    
    public void Update()
    {
        if (parent.MyAttackTime >= attackCooldown && !parent.IsAttacking)
        {
            parent.MyAttackTime = 0;

            parent.StartCoroutine(Attack());
        }

        if(parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance >= parent.MyAttackRange+extraRange && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack()
    {
        int[] numbers;

        numbers = DamageTextManager.MyInstance.seperateNumber(parent.MyDamage);

        for (int i = 0; i < numbers.Length; i++)
        {
            float adj = numbers.Length / 2;

            if (numbers.Length % 2 == 0)
                adj -= 0.5f;

            Vector3 tmpVector3 = new Vector3(0.4f * (i - adj), 0, 0);

            tmpVector3 += parent.MyTarget.transform.position;

            DamageTextManager.MyInstance.CreateText(tmpVector3, numbers[i], DMGTEXTTYPE.DAMAGED, false);
        }

        Player.MyInstance.MyHealth.MyCurrentValue -= parent.MyDamage;

        parent.IsAttacking = true;

        parent.MyAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(0).length);

        parent.IsAttacking = false;
    }
}
