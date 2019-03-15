using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Character 
{
    [SerializeField]
    private Animator[] animators;

    public Animator[] MyAnimators
    {
        get
        {
            return animators;
        }
    }

    [SerializeField]
    private int Grade;

    [SerializeField]
    private Stat mana;

    [SerializeField]
    private Exp exp;

    [SerializeField]
    private float initMana;

    public float MyInitMana
    {
        get
        {
            return initMana;
        }
    }

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 0;

    [SerializeField]
    private Block[] blocks;

    private bool playerExists;

    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Text playerLevel;

    private int strength;
    private int vitality;
    private int agility;
    private int intellegence;

    private int min_atk;
    private int max_atk;
    private float acc_rate;
    private float strike_rate;
    private float dodge_rate;
    private float magic_res;

    private int statpoint;
    private int temp_str;
    private int temp_vit;
    private int temp_agi;
    private int temp_int;

    private string Class;

    public string MyClass
    {
        get
        {
            return Class;
        }
    }

    public int MyStr
    {
        get
        {
            return strength;
        }
    }

    public int MyMinAtk
    {
        get
        {
            return min_atk;
        }
    }

    public int MyMaxAtk
    {
        get
        {
            return max_atk;
        }
    }

    public float MyAccRate
    {
        get
        {
            return acc_rate;
        }
    }

    public float MyStrikeRate
    {
        get
        {
            return strike_rate;
        }
    }

    public float MyDodgeRate
    {
        get
        {
            return dodge_rate;
        }
    }

    public float MyMagicRes
    {
        get
        {
            return magic_res;
        }
    }

    public int MyVit
    {
        get
        {
            return vitality;
        }
    }

    public int MyAgi
    {
        get
        {
            return agility;
        }
    }

    public int MyInt
    {
        get
        {
            return intellegence;
        }
    }

    public int MyStatPoint
    {
        get
        {
            return statpoint;
        }
    }

    public int MyGrade
    {
        get
        {
            return Grade;
        }
    }

    public Stat MyMana
    {
        get
        {
            return mana;
        }
    }

    public Exp MyExp
    {
        get
        {
            return exp;
        }
    }

    public string startPoint;

    private IInteractable interactable;

    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }

    public bool moving;

    /*private List<Enemy> attackers = new List<Enemy>();

    public List<Enemy> MyAttackers
    {
        get
        {
            return attackers;
        }

        set
        {
            attackers = value;
        }
    }*/

    protected override void Start()
    {
        DefaultValue();

        if (!playerExists)
        {
            playerExists = true;
        }

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        if (direction != Vector2.zero)
        {
            GameManager.MyInstance.calculatedLists();
        }
        statUpdate();
        checkLevelUp();
        base.Update();
	}

    public void CreateNewCharacter(string name, string Class, int level, int str, int vit, int agi, int intel)
    {

        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);
        exp.Initialize(0.0f, Mathf.Floor(50 * level * Mathf.Pow(level, 0.5f)));
        statinit(str, vit, agi, intel, 0);
        Name = name;
        playerName.text = name;
        setLevel(level);
        this.Class = Class;

        playerLevel.text = "Lv." + level;
    }

    public void LoadCharacterInfo(string name, string Class, float xp, float hp, float maxhp, float mp, float maxmp, int level, int str, int vit, int agi, int intel, int bonus, float x, float y)
    {
        Name = name;
        playerName.text = Name;
        setLevel(level);
        UpdateLevelText();
        MyHealth.Initialize(hp, maxhp);
        MyMana.Initialize(mp, maxmp);
        MyExp.Initialize(xp, Mathf.Floor(50 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
        statinit(str, vit, agi, intel, bonus);
        transform.position = new Vector2(x, y);
        this.Class = Class;
    }

    private void DefaultValue()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);
        exp.Initialize(0.0f, Mathf.Floor(50 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
        statinit(1, 1, 1, 1, 0);
        playerName.text = "";
        Class = "";
        playerLevel.text = "Lv." + MyLevel;
    }

    private void statinit(int str, int vit, int agi, int intel, int bonus)
    {
        strength = str;
        vitality = vit;
        agility = agi;
        intellegence = intel;
        statpoint = bonus;
    }

    private void statUpdate()
    {
        min_atk = strength * 2 / 5;
        max_atk = strength * 4 / 5;
        Def = strength / 5;
        acc_rate = 50 + (agility / 5 * 0.3f);
        strike_rate = agility / 5 * 0.1f;
        dodge_rate = agility / 5 * 0.2f;
        magic_res = 0f;
        health.MyMaxValue = MyInitHealth + (vitality - 4) * 16;
        mana.MyMaxValue = MyInitMana + (intellegence - 4) * 12;
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyBindManager.MyInstance.KeyBinds["UP"]))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyBindManager.MyInstance.KeyBinds["DOWN"]))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyBindManager.MyInstance.KeyBinds["LEFT"]))
        {
            direction += Vector2.left;
            RightPressed = 0;
        }

        if (Input.GetKey(KeyBindManager.MyInstance.KeyBinds["RIGHT"]))
        {
            direction += Vector2.right;
            RightPressed = 1;
        }

        if (direction != Vector2.zero)
        {
            StopAttack();
        }
        
        if (Input.GetKey(KeyBindManager.MyInstance.KeyBinds["ATTACK"]))
        {
            CastSpell("기본공격");
        }

        foreach(string slot in KeyBindManager.MyInstance.SlotBinds.Keys)
        {
            if (Input.GetKeyDown(KeyBindManager.MyInstance.SlotBinds[slot]))
            {
                UIManager.MyInstance.ClickSlotButton(slot);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            gainExp(600);
        }

        exitIndex = (int)RightPressed;
    }

    public int calcDamage(int damage_percentage)
    {
        int dmg = Random.Range(damage_percentage * MyMinAtk / 100, damage_percentage * MyMaxAtk / 100);
        return dmg;
    }

    private IEnumerator Attack(string spellName)
    {
        int[] numbers;

        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);

        IsAttacking = true;

        MyAnimator.SetBool("Attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            int damage = newSpell.calcDamage();
            bool strike = IsStrikeAttack();
            if (strike)
            {
                damage *= 2;
            }

            s.Initialize(currentTarget, damage, newSpell.MySpellNumber, transform);

            numbers = DamageTextManager.MyInstance.seperateNumber(damage);

            for (int i = 0; i < numbers.Length; i++)
            {
                float adj = numbers.Length/2;

                if (numbers.Length % 2 == 0)
                    adj -= 0.5f;

                Vector3 tmpVector3 = new Vector3(0.4f*(i-adj), 0, 0);

                tmpVector3 += MyTarget.transform.position;

                DamageTextManager.MyInstance.CreateText(tmpVector3, numbers[i], DMGTEXTTYPE.ATTACK, strike);

            }
        }

        StopAttack();
    }

    public void CastSpell(string spellName)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Enemy>().IsAlive && !IsAttacking && InLineOfSight())
        {
            actionRoutine = StartCoroutine(Attack(spellName));
        }
    }

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetdir = (MyTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetdir, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        }

        return false; 
    }

    private void Block()
    {
        foreach(Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

    public void StopAttack()
    {
        SpellBook.MyInstance.StopCasting();

        IsAttacking = false;

        MyAnimator.SetBool("Attack", IsAttacking);

        if (actionRoutine != null)
        {
            StopCoroutine(actionRoutine);
        }
    }

    public void checkLevelUp()
    {
        if (exp.MyCurrentValue >= exp.MyMaxValue)
        {
            exp.MyCurrentValue -= exp.MyMaxValue;
            LevelUp();
            exp.Initialize(exp.MyCurrentValue, Mathf.Floor(50 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
            playerLevel.text = "Lv." + MyLevel;
            health.MyCurrentValue = health.MyMaxValue;
            mana.MyCurrentValue = mana.MyMaxValue;
            statpoint += 5;
        }
    }

    public void gainExp(int exp)
    {
        this.exp.MyCurrentValue += exp;
    }

    public void Interact()
    {
        if (interactable != null)
        { 
            interactable.Interact();
        }
        canMove = false;
    }

    public void StopInteract()
    {
        interactable.StopInteract();
        canMove = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interactable = collision.GetComponent<IInteractable>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            if (interactable != null)
            {
                interactable.StopInteract();
                interactable = null;
            }
        }
    }

    public bool IsStrikeAttack()
    {
        float tmp = Random.Range(0.1f, 100.0f);

        if (tmp <= strike_rate)
        {
            return true;
        }

        return false;
    }

    public void GetMana(int mana)
    {
        int[] numbers;

        MyMana.MyCurrentValue += mana;

        numbers = DamageTextManager.MyInstance.seperateNumber(mana);

        for (int i = 0; i < numbers.Length; i++)
        {
            float adj = numbers.Length / 2;

            if (numbers.Length % 2 == 0)
                adj -= 0.5f;

            Vector3 tmpVector3 = new Vector3(0.4f * (i - adj), 0, 0);

            tmpVector3 += MyTarget.transform.position;

            DamageTextManager.MyInstance.CreateText(tmpVector3, numbers[i], DMGTEXTTYPE.MANAHEAL, false);
        }
    }

    public void Gather(string skillName)
    {
        if (!IsAttacking)
        {
            actionRoutine = StartCoroutine(GatherRoutine(skillName));
        }
    }

    private IEnumerator GatherRoutine(string skillName)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(skillName);

        IsAttacking = true;

        MyAnimator.SetBool("Attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        StopAttack();
    }

    //STATUS FUNCTION
    public void StatUp(GameObject button)
    {
        SoundManager.MyInstance.onClickSmallButton();

        if (statpoint > 0)
        {
            statpoint--;

            switch (button.name)
            {
                case "공격_능력":
                    temp_str++;
                    strength++;
                    break;
                case "체력_능력":
                    temp_vit++;
                    vitality++;
                    break;
                case "민첩_능력":
                    temp_agi++;
                    agility++;
                    break;
                case "지력_능력":
                    temp_int++;
                    intellegence++;
                    break;
            }
        }
    }

    public void StatDown(GameObject button)
    {
        SoundManager.MyInstance.onClickSmallButton();

        switch (button.name)
        {
            case "공격_능력":
                if (temp_str > 0)
                {
                    temp_str--;
                    strength--;
                    statpoint++;
                }
                break;
            case "체력_능력":
                if (temp_vit > 0)
                {
                    temp_vit--;
                    vitality--;
                    statpoint++;
                }
                break;
            case "민첩_능력":
                if (temp_agi > 0)
                {
                    temp_agi--;
                    agility--;
                    statpoint++;
                }

                break;
            case "지력_능력":
                if (temp_int > 0)
                {
                    temp_int--;
                    intellegence--;
                    statpoint++;
                }
                break;
        }
    }

    public void stat_apply()
    {
        temp_str = 0;
        temp_vit = 0;
        temp_agi = 0;
        temp_int = 0;
    }

    public void stat_restore()
    {
        statpoint += temp_str;
        strength -= temp_str;

        statpoint += temp_vit;
        vitality -= temp_vit;

        statpoint += temp_agi;
        agility -= temp_agi;

        statpoint += temp_int;
        intellegence -= temp_int;

        stat_apply();
    }

    public void UpdateLevelText()
    {
        playerLevel.text = "Lv." + MyLevel.ToString();
    }

    /*public void AddAttacker(Enemy enemy)
    {
        if (!MyAttackers.Contains(enemy))
        {
            MyAttackers.Add(enemy);
        }
    }*/
}
