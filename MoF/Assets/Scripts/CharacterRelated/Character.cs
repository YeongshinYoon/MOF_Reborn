﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {
    [SerializeField]
    private float speed;

    protected float RightPressed;

    public Animator MyAnimator { get; set; }

    protected Vector2 direction;

    protected Rigidbody2D myRigidbody;

    public bool IsAttacking { get; set; }

    protected Coroutine attackRoutine;

    [SerializeField]
    private float initHealth;

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    protected Stat health;

    [SerializeField]
    private string Name;

    [SerializeField]
    private int Level;

    [SerializeField]
    protected int Def;

    public bool canMove;

    [SerializeField]
    private bool IsEnemy;

    public Transform MyTarget { get; set; }

    public float MyInitHealth
    {
        get
        {
            return initHealth;
        }
    }

    public Stat MyHealth
    {
        get
        {
            return health;
        }
    }

    public string MyName
    {
        get
        {
            return Name;
        }
    }

    public int MyLevel
    {
        get
        {
            return Level;
        }
    }

    private bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return health.MyCurrentValue > 0;
        }
    }

    public int MyDef
    {
        get
        {
            return Def;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
        health.Initialize(initHealth, initHealth);

        MyAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        canMove = true;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();

        Physics2D.IgnoreLayerCollision(0, 9);
    }

    public void Move()
    {
        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }

        if (IsAlive)
        {
            myRigidbody.velocity = direction.normalized * Speed;
        }
    }

    public void HandleLayers()
    {
        if (IsAlive)
        {
            if (IsMoving)
            {
                MyAnimator.SetFloat("x", direction.x);
                MyAnimator.SetFloat("y", direction.y);

                MyAnimator.SetFloat("RightPressed", RightPressed);
            }
        }
    }

    public virtual void TakeDamage(float damage, Transform source)
    {
        if (damage - MyDef > 0)
        {
            health.MyCurrentValue = health.MyCurrentValue - damage + MyDef;
        }

        if (health.MyCurrentValue <= 0)
        {
            myRigidbody.velocity = Vector2.zero;
            MyAnimator.SetTrigger("die");

            if (this is Enemy)
            {
                Player.MyInstance.gainExp(XPManager.CalculateXP((this as Enemy)));
            }
        }
    }

    public void LevelUp()
    {
        Level++;
    }

    public void GetHealth(int health)
    {
        int[] numbers;

        MyHealth.MyCurrentValue += health;

        numbers = DamageTextManager.MyInstance.seperateNumber(health);

        for (int i = 0; i < numbers.Length; i++)
            DamageTextManager.MyInstance.CreateText(transform.position, numbers[i], DMGTEXTTYPE.HPHEAL, false);
    }


}
