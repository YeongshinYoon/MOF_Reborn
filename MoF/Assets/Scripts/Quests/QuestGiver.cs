using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private Quest[] quests;

    public Quest[] MyQuests
    {
        get
        {
            return quests;
        }
    }

    [SerializeField]
    private GameObject QuestStart, QuestEnd;

    private void Start()
    {
        quests = QuestManager.MyInstance.GetQuests(gameObject.name);
    }

    private void Update()
    {
        UpdateQuestStatus();
    }

    public void UpdateQuestStatus()
    {
        bool start_trigger = false;
        bool end_trigger = false;

        foreach (GameObject questObject in Questlog.MyInstance.MyQuestObjects)
        {
            Quest tmp = questObject.GetComponent<QuestScript>().MyQuest;

            if (tmp.IsCompletable && tmp.MyDestination == gameObject.name)
            {
                QuestEnd.gameObject.SetActive(true);
                end_trigger = true;
            }
        }

        foreach (Quest quest in quests)
        {
            if (quest.IsAcceptable)
            {
                QuestStart.gameObject.SetActive(true);
                start_trigger = true;
            }
        }

        if (!start_trigger)
        {
            QuestStart.gameObject.SetActive(false);
        }

        if (!end_trigger)
        {
            QuestEnd.gameObject.SetActive(false);
        }
    }

    public void setQuestMarkOff()
    {
        QuestStart.gameObject.SetActive(false);
        QuestEnd.gameObject.SetActive(false);
    }
}
