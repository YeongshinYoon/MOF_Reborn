using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
 public class Spell : IUsable, IMovable, IDescribable {
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage_percentage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private string description;

    [SerializeField]
    private int spellNumber;

    public int MySpellNumber
    {
        get
        {
            return spellNumber;
        }
    }

    public string MyName
    {
        get
        {
            return name;
        }
    }

    public int MyDamagePercentage
    {
        get
        {
            return damage_percentage;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public float MySpeed
    {
        get
        {
            return speed;
        }
    }

    public float MyCastTime
    {
        get
        {
            return castTime;
        }        
    }

    public GameObject MySpellPrefab
    {
        get
        {
            return spellPrefab;
        }
    }

    public string GetDescription()
    {
        return string.Format("{0}\n캐스팅 시간 : {1} 초\n{2}\n{3}%%의 데미지를 입힌다.", name, castTime, description, damage_percentage);
    }

    public void use()
    {
        Player.MyInstance.CastSpell(MyName);
    }

    public int calcDamage()
    {
        int dmg = UnityEngine.Random.Range(damage_percentage * Player.MyInstance.MyMinAtk / 100, damage_percentage * Player.MyInstance.MyMaxAtk / 100);
        return dmg;
    }
}
