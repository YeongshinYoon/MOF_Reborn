using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {
    private static QuestManager instance;

    public static QuestManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Quest[] quests;

    public Quest[] MyQuests
    {
        get
        {
            return quests;
        }
    }

    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public Quest[] GetQuests(string npcName)
    {
        int tmpcount = 0;
        int tmpindex = 0;

        foreach(Quest quest in quests)
        {
            if (quest.MyQuestGiverName == npcName)
            {
                tmpcount++;
            }
        }

        Quest[] tmpquests = new Quest[tmpcount];

        foreach(Quest quest in quests)
        {
            if (quest.MyQuestGiverName == npcName)
            {
                tmpquests[tmpindex++] = quest;
            }
        }

        return tmpquests;
    }
}
