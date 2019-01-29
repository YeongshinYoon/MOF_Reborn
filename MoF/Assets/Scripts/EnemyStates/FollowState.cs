using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (parent.IsDamaged && parent.MyTarget != null)
        {
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.MyTarget.position, parent.Speed * Time.deltaTime);

            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance <= parent.MyAttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }
        /*if (!parent.InRange)
        {
            parent.ChangeState(new EvadeState());
        }*/
    }
}
