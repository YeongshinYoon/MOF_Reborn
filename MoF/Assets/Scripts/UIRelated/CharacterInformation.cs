using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInformation : MonoBehaviour {
    private static CharacterInformation instance;

    public static CharacterInformation MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterInformation>();
            }

            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Text Name;

    [SerializeField]
    private Text Spouse;

    [SerializeField]
    private Text Class;

    [SerializeField]
    private Text Level;

    [SerializeField]
    private Text Grade;

    [SerializeField]
    private Text Title;

    [SerializeField]
    private Stat HP;

    [SerializeField]
    private Stat MP;

    [SerializeField]
    private Exp EXP;

    [SerializeField]
    private Text strength;

    [SerializeField]
    private Text vitality;

    [SerializeField]
    private Text agility;

    [SerializeField]
    private Text intellegence;

    [SerializeField]
    private Text statpoint;

    [SerializeField]
    private Text atk;

    [SerializeField]
    private Text def;

    [SerializeField]
    private Text acc_rate;

    [SerializeField]
    private Text strike_rate;

    [SerializeField]
    private Text dodge_rate;

    [SerializeField]
    private Text magic_res;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        strength.text = Player.MyInstance.MyStr + "";
        vitality.text = Player.MyInstance.MyVit + "";
        agility.text = Player.MyInstance.MyAgi + "";
        intellegence.text = Player.MyInstance.MyInt + "";
        statpoint.text = Player.MyInstance.MyStatPoint + "";
        HP.MyCurrentValue = Player.MyInstance.MyHealth.MyCurrentValue;
        HP.MyMaxValue = Player.MyInstance.MyHealth.MyMaxValue;
        MP.MyCurrentValue = Player.MyInstance.MyMana.MyCurrentValue;
        MP.MyMaxValue = Player.MyInstance.MyMana.MyMaxValue;
        EXP.MyCurrentValue = Player.MyInstance.MyExp.MyCurrentValue;
        EXP.MyMaxValue = Player.MyInstance.MyExp.MyMaxValue;
        Name.text = Player.MyInstance.MyName;
        Spouse.text = "없음";
        Class.text = Player.MyInstance.MyClass;
        Level.text = Player.MyInstance.MyLevel + "";
        Grade.text = Player.MyInstance.MyGrade + "";
        Title.text = "없음";
        atk.text = Player.MyInstance.MyMinAtk + " ~ " + Player.MyInstance.MyMaxAtk;
        def.text = Player.MyInstance.MyDef + "";
        acc_rate.text = Player.MyInstance.MyAccRate + "%";
        strike_rate.text = Player.MyInstance.MyStrikeRate + "%";
        dodge_rate.text = Player.MyInstance.MyDodgeRate + "%";
        magic_res.text = Player.MyInstance.MyMagicRes + "%";
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
}
