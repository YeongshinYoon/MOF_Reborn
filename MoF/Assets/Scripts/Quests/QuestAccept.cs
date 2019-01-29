using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAccept : MonoBehaviour
{
    private static QuestAccept instance;

    public static QuestAccept MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestAccept>();
            }

            return instance;
        }
    }

    [SerializeField]
    private GameObject completeBtn, acceptBtn, acceptableBtn, completableBtn, closeBtn, questDescription;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private QuestGiver questGiver;

    [SerializeField]
    private GameObject acceptableQuestPrefab, completableQuestPrefab;

    [SerializeField]
    private Transform questArea;

    private Quest selectedQuest;

    private QuestAcceptScript[] acceptableQuestScripts;

    private QuestClearScript[] completableQuestScripts;

    private List<GameObject> questObjects = new List<GameObject>();

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text description;

    [SerializeField]
    private Text objective;

    [SerializeField]
    private Text npcName;

    [SerializeField]
    private Image questGiverImage;

    [SerializeField]
    private Text reward_EXP;

    [SerializeField]
    private Text reward_RIBI;

    [SerializeField]
    private Scrollbar scrollbar;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptQuest()
    {
        foreach (GameObject questObject in questObjects)
        {
            if (questObject.GetComponent<QuestAcceptScript>().MyQuest.MyTitle == selectedQuest.MyTitle)
            {
                if (selectedQuest.MyQuestItem != null)
                {
                    InventoryScript.MyInstance.AddItem(selectedQuest.MyQuestItem);
                }
                selectedQuest.IsAccepted = true;
                selectedQuest.IsCompleted = false;
                questObjects.Remove(questObject);
                Destroy(questObject);
                Back();
                Questlog.MyInstance.AcceptQuest(selectedQuest);
                break;
            }
        }

    }

    public void CompleteQuest()
    {
        /*for (int i = 0; i < questGiver.MyQuests.Length; i++)
        {
            if (selectedQuest == questGiver.MyQuests[i])
            {
                questGiver.MyQuests[i] = null;
            }
        }*/

        foreach (GameObject questObject in questObjects)
        {
            if (questObject.GetComponent<QuestClearScript>().MyQuest.MyTitle == selectedQuest.MyTitle)
            {
                foreach(CollectObjective o in selectedQuest.MyCollectObjectives)
                {
                    InventoryScript.MyInstance.itemCountChangedEvent -= o.UpdateItemCount;
                    o.Complete();
                }

                foreach (KillObjective o in selectedQuest.MyKillObjectives)
                {
                    GameManager.MyInstance.killConfirmEvent -= o.UpdateKillCount;
                }
                selectedQuest.IsCompleted = true;
                questObjects.Remove(questObject);
                Destroy(questObject);
                Back();
                break;
            }
        }

        Player.MyInstance.gainExp(selectedQuest.MyRewardEXP);
        InventoryScript.MyInstance.gainRibi(selectedQuest.MyRewardRIBI);

        Questlog.MyInstance.CompleteQuest(selectedQuest);
    }

    public void showCompletableQuests()
    {
        completableBtn.GetComponentInChildren<Text>().color = Color.yellow;

        acceptableBtn.GetComponentInChildren<Text>().color = Color.white;

        foreach (GameObject questObject in questObjects)
        {
            Destroy(questObject);
        }

        questObjects.Clear();

        foreach (GameObject questObject in Questlog.MyInstance.MyQuestObjects)
        {
            Quest tmp = questObject.GetComponent<QuestScript>().MyQuest;
            if (tmp.IsAccepted && tmp.IsCompletable && !tmp.IsCompleted && tmp.MyDestination == questGiver.name)
            {
                GameObject go = Instantiate(completableQuestPrefab, questArea);
                questObjects.Add(go);

                go.GetComponent<QuestClearScript>().MyQuest = tmp;

                go.GetComponentInChildren<Text>().text = tmp.MyTitle;
            }
        }
    }

    public void showAcceptableQuests_btn()
    {
        showAcceptableQuests(DialogueBox.MyInstance.currentNPC.GetComponent<QuestGiver>());
    }

    public void showAcceptableQuests(QuestGiver questGiver)
    {
        completeBtn.SetActive(false);

        completableBtn.GetComponentInChildren<Text>().color = Color.white;

        acceptableBtn.GetComponentInChildren<Text>().color = Color.yellow;

        this.questGiver = questGiver;

        foreach (GameObject questObject in questObjects)
        {
            Destroy(questObject);
        }

        questObjects.Clear();

        foreach (Quest quest in questGiver.MyQuests)
        {
            if (quest != null)
            {
                if (!quest.IsAccepted)
                {
                    if (quest.IsAcceptable)
                    {
                        GameObject go = Instantiate(acceptableQuestPrefab, questArea);

                        go.GetComponent<QuestAcceptScript>().MyQuest = quest;

                        go.GetComponentInChildren<Text>().text = quest.MyTitle;

                        go.GetComponentsInChildren<Text>()[1].text = "요구레벨 " + quest.MyLevel;

                        questObjects.Add(go);
                    }
                }
            }
        }
    }

    public void Select(Quest quest)
    {
        selectedQuest = quest;
    }

    public void UpdateSelected()
    {
        Select(selectedQuest);
    }

    public void Open()
    {
        showAcceptableQuests(DialogueBox.MyInstance.currentNPC.GetComponent<QuestGiver>());
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public void Close()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void Back()
    {
        questDescription.SetActive(false);
        closeBtn.SetActive(true);
        acceptableBtn.SetActive(true);
        completableBtn.SetActive(true);
        questArea.gameObject.SetActive(true);
    }

    public void ShowQuestInfo(Quest quest, int questType)
    {
        //1 : accept
        //2 : complete

        selectedQuest = quest;

        acceptableBtn.SetActive(false);
        completableBtn.SetActive(false);
        closeBtn.SetActive(false);
        questArea.gameObject.SetActive(false);
        questDescription.SetActive(true);

        string obj_str = "";

        if (quest.MyCollectObjectives.Length == 0 && quest.MyKillObjectives.Length == 0)
        {
            obj_str = "No Objectives";
        }


        foreach (Objective obj in quest.MyCollectObjectives)
        {
            obj_str += obj.MyType + " : " + obj.MyCurrentAmount + " / " + obj.MyAmount + "\n";
        }

        foreach (Objective obj in quest.MyKillObjectives)
        {
            obj_str += obj.MyType + " : " + obj.MyCurrentAmount + " / " + obj.MyAmount + "\n";
        }

        title.text = quest.MyTitle;
        if (questType == 1)
        {
            acceptBtn.SetActive(true);
            completeBtn.SetActive(false);
            npcName.text = quest.MyQuestGiver;
            questGiverImage.sprite = quest.MyQuestGiverImage;
            questGiverImage.gameObject.SetActive(true);
            description.text = quest.MyDescription;
        }
        else if (questType == 2)
        {
            acceptBtn.SetActive(false);
            completeBtn.SetActive(true);
            npcName.text = quest.MyDestination;
            questGiverImage.sprite = quest.MyDestinationImage;
            questGiverImage.gameObject.SetActive(true);
            description.text = quest.MyClearDescription;
        }

        objective.text = obj_str;
        reward_EXP.text = quest.MyRewardEXP + "";
        reward_RIBI.text = quest.MyRewardRIBI + "";
        scrollbar.value = 1;
    }
}
