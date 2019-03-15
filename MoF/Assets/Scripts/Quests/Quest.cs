using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest : MonoBehaviour
{
    [SerializeField]
    private int questNumber;

    public int MyQuestNumber
    {
        get
        {
            return questNumber;
        }
    }

    [SerializeField]
    private bool IsLinked;

    [SerializeField]
    private int LinkedQuestNumber;

    public int MyLinkedQuestNumber
    {
        get
        {
            return LinkedQuestNumber;
        }
    }

    [SerializeField]
    private int level;

    public int MyLevel
    {
        get
        {
            return level;
        }
    }

    [SerializeField]
    private string title;

    public string MyTitle
    {
        get
        {
            return title;
        }
    }

    [SerializeField]
    private string description;

    public string MyDescription
    {
        get
        {
            return description;
        }
    }

    [SerializeField]
    private string clearDescription;

    public string MyClearDescription
    {
        get
        {
            return clearDescription;
        }
    }

    [SerializeField]
    private Item questItem;

    public Item MyQuestItem
    {
        get
        {
            return questItem;
        }
    }

    [SerializeField]
    private string questGiverName;

    public string MyQuestGiverName
    {
        get
        {
            return questGiverName;
        }
    }

    [SerializeField]
    private string destination;

    public string MyDestination
    {
        get
        {
            return destination;
        }
    }

    [SerializeField]
    private Sprite questGiverImage;

    public Sprite MyQuestGiverImage
    {
        get
        {
            return questGiverImage;
        }
    }

    [SerializeField]
    private Sprite destinationImage;

    public Sprite MyDestinationImage
    {
        get
        {
            return destinationImage;
        }
    }

    [SerializeField]
    private int reward_EXP;

    public int MyRewardEXP
    {
        get
        {
            return reward_EXP;
        }
    }

    [SerializeField]
    private int reward_RIBI;

    public int MyRewardRIBI
    {
        get
        {
            return reward_RIBI;
        }
    }

    [SerializeField]
    private CollectObjective[] collectObjectives;

    public CollectObjective[] MyCollectObjectives
    {
        get
        {
            return collectObjectives;
        }
    }

    [SerializeField]
    private KillObjective[] killObjectives;

    public KillObjective[] MyKillObjectives
    {
        get
        {
            return killObjectives;
        }
    }

    public QuestScript MyQuestScript { get; set; }

    public bool IsAccepted = false;

    public bool IsAcceptable
    {
        get
        {
            if (!IsAccepted && level <= Player.MyInstance.MyLevel && (!IsLinked || (IsLinked && QuestManager.MyInstance.MyQuests[LinkedQuestNumber].IsCompleted)))
            {
                return true;
            }
            else return false;
        }
    }

    public bool IsCompletable
    {
        get
        {
            if (IsAccepted && !IsCompleted)
            {
                foreach (Objective o in collectObjectives)
                {
                    if (!o.IsComplete)
                    {
                        return false;
                    }
                }

                foreach (Objective o in killObjectives)
                {
                    if (!o.IsComplete)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }

    public bool IsCompleted = false;

    public bool messagePopuped;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int MyAmount
    {
        get
        {
            return amount;
        }
    }

    public int MyCurrentAmount
    {
        get
        {
            return currentAmount;
        }

        set
        {
            currentAmount = value;
        }
    }

    public string MyType
    {
        get
        {
            return type;
        }
    }

    public bool IsComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount(Item item)
    {
        if (MyType == item.MyTitle)
        {
            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(item.MyTitle);

            if (MyCurrentAmount <= MyAmount)
            {
                MessageFeedManager.MyInstance.WriteMessage(string.Format("{0} : {1} / {2}", item.MyTitle/*MyType*/, MyCurrentAmount, MyAmount));
            }

            Questlog.MyInstance.UpdateSelected();
            Questlog.MyInstance.CheckCompletion();
        }
    }

    public void UpdateItemCount()
    {
        MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(MyType);
        Questlog.MyInstance.UpdateSelected();
        Questlog.MyInstance.CheckCompletion();
    }

    public void Complete()
    {
        Stack<Item> items = InventoryScript.MyInstance.GetItems(MyType, MyAmount);

        foreach (Item item in items)
        {
            item.MustRemove();
            if (item.MySlot.IsEmpty)
            {
                item.MySlot = InventoryScript.MyInstance.FindItemSlot(item);
            }
        }
    }
}

[System.Serializable]
public class KillObjective : Objective
{
    public void UpdateKillCount(Enemy enemy)
    {
        if (MyType == enemy.MyName)
        {
            MyCurrentAmount++;

            if (MyCurrentAmount <= MyAmount)
            {
                MessageFeedManager.MyInstance.WriteMessage(string.Format("{0} : {1} / {2}", enemy.MyName, MyCurrentAmount, MyAmount));
            }

            Questlog.MyInstance.UpdateSelected();
            Questlog.MyInstance.CheckCompletion();
        }
    }
}