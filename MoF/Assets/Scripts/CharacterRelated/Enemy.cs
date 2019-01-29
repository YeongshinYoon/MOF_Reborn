using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);
public delegate void CharacterRemoved();

public class Enemy : Character, IInteractable 
{
    public event HealthChanged healthChanged;

    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private float MonsterNumber;

    private IState currentState;

    public IState MyCurrentState
    {
        get
        {
            return currentState;
        }
    }

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }
    
    public Vector3 MyStartPosition { get; set; }

    /*[SerializeField]
    private float initAggroRange;

    public float MyAggroRange { get; set; }*/
    
    [SerializeField]
    private int min_damage;

    [SerializeField]
    private int max_damage;

    [SerializeField]
    private LootTable lootTable;

    private bool Damaged;

    public bool IsDamaged
    {
        get
        {
            return Damaged;
        }
    }

    public int MyDamage
    {
        get
        {
            return Random.Range(min_damage, max_damage);
        }
    }

    private bool moving;

    [SerializeField]
    private float timeToMove;

    private float curTimeToMove;

    [SerializeField]
    private float timeBetweenMove;

    private float curTimeBetweenMove;

    private Vector3 randDir;

    /*public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }*/

    protected void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetFloat("MonsterNumber", MonsterNumber);
        MyStartPosition = transform.position;
        curTimeBetweenMove = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        curTimeToMove = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        //MyAggroRange = initAggroRange;
        MyAttackRange = 0.5f;
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }

            currentState.Update();
        }
        base.Update();
    }

    public Transform Select()
    {
        healthGroup.alpha = 1;

        return hitBox;
    }

    public void DeSelect()
    {
        healthGroup.alpha = 0;

        healthChanged -= UIManager.MyInstance.updateTargetFrame;
        characterRemoved -= UIManager.MyInstance.hideTargetFrame;
    }

    public override void TakeDamage(float damage, Transform source)
    {
        if (!(currentState is EvadeState))
        {
            Damaged = true;

            SetTarget(source);

            base.TakeDamage(damage, source);

            OnHealthChanged(health.MyCurrentValue);

            if (health.MyCurrentValue <= 0)
            {
                GameManager.MyInstance.OnKillConfirmed(this);
                Interact();
            }
        }
    }
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);

            //MyAggroRange = initAggroRange;
            //MyAggroRange += distance;

            MyTarget = target;
        }
    }

    public void Reset()
    {
        MyTarget = null;
        //MyAggroRange = initAggroRange;
        MyHealth.MyCurrentValue = MyHealth.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(gameObject);
    }

    public void Interact()
    {
        if (!IsAlive)
        {
            lootTable.ShowLoot();
        }
    }

    public void StopInteract()
    {
        LootWindow.MyInstance.Close();
    }

    public void randMove()
    {
        if (moving)
        {
            curTimeToMove -= Time.deltaTime;
            transform.position += randDir.normalized * Speed * Time.deltaTime;

            if (curTimeToMove < 0f)
            {
                moving = false;
                curTimeBetweenMove = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            curTimeBetweenMove -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;

            if (curTimeBetweenMove < 0f)
            {
                moving = true;
                curTimeToMove = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                randDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
        }
    }
}
