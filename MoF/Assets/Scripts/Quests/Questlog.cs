using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questlog : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private static Questlog instance;

    public static Questlog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Questlog>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text description;

    [SerializeField]
    private Text objective;

    [SerializeField]
    private Text questGiver;

    [SerializeField]
    private Image questGiverImage;

    [SerializeField]
    private Text reward_EXP;

    [SerializeField]
    private Text reward_RIBI;

    private List<QuestScript> questScripts = new List<QuestScript>();

    public List<QuestScript> MyQuestScripts
    {
        get
        {
            return questScripts;
        }
    }

    private List<GameObject> questObjects = new List<GameObject>();

    public List<GameObject> MyQuestObjects
    {
        get
        {
            return questObjects;
        }
    }

    Quest selected;

	// Use this for initialization
	void Start () 
    {
        questGiverImage.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void AcceptQuest(Quest quest)
    {
        foreach (CollectObjective o in quest.MyCollectObjectives)
        {
            InventoryScript.MyInstance.itemCountChangedEvent += o.UpdateItemCount;
            o.UpdateItemCount();
        }

        foreach (KillObjective o in quest.MyKillObjectives)
        {
            GameManager.MyInstance.killConfirmEvent += o.UpdateKillCount;
        }

        GameObject go = Instantiate(questPrefab, questParent);
        questObjects.Add(go);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;
        questScripts.Add(qs);

        go.GetComponentInChildren<Text>().text = quest.MyTitle;

        CheckCompletion();
    }

    public void CompleteQuest(Quest quest)
    {
        foreach (GameObject questObject in questObjects)
        {
            if (questObject.GetComponent<QuestScript>().MyQuest.MyTitle == quest.MyTitle)
            {
                questObjects.Remove(questObject);
                Destroy(questObject);
                questScripts.Remove(questObject.GetComponent<QuestScript>());
                break;
            }
        }
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
            }

            string obj_str = "";

            selected = quest;

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
            description.text = quest.MyDescription;
            objective.text = obj_str;
            questGiver.text = quest.MyQuestGiverName;
            questGiverImage.sprite = quest.MyQuestGiverImage;
            questGiverImage.gameObject.SetActive(true);
            reward_EXP.text = quest.MyRewardEXP + "";
            reward_RIBI.text = quest.MyRewardRIBI + "";
        }
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
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
            ClearDescription();
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    public void CheckCompletion()
    {
        foreach (QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    public void ClearDescription()
    {
        if (selected != null)
        {
            selected.MyQuestScript.DeSelect();
        }
        selected = null;
        title.text = "";
        description.text = "";
        objective.text = "";
        questGiver.text = "";
        questGiverImage.sprite = null;
        questGiverImage.gameObject.SetActive(false);
        reward_EXP.text = "";
        reward_RIBI.text = "";
    }

    public void AbandonQuest()
    {
        //Removes the quest from the quest log
        //Remember to remove the quest from quest list
    }
}
