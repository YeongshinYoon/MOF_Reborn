using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {
    private static PlayerStat instance;

    public static PlayerStat MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerStat>();
            }

            return instance;
        }
    }

    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int currentExp;
    [SerializeField]
    private int[] toLevelUp;

    public int MyCurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

    public int MyCurrentExp
    {
        get
        {
            return currentExp;
        }
    }

    public int[] MyToLevelUp
    {
        get
        {
            return toLevelUp;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentExp >= toLevelUp[currentLevel])
        {
            currentExp -= toLevelUp[currentLevel];
            currentLevel++;
        }
	}

    public void gainExp(int exp)
    {
        currentExp += exp;
    }
}
