using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour 
{
    private static SaveManager instance;

    public static SaveManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
            }

            return instance;
        }
    }

    private Item[] items;

    [SerializeField]
    private SlotButton[] slotButtons;

    //창고
    //private Chest[] chests;

    private EquipmentButton[] equipment;

    [SerializeField]
    private SavedGame[] saveSlots;

    public SavedGame[] MySaveSlots
    {
        get
        {
            return saveSlots;
        }
    }

    [SerializeField]
    private SavedGame[] ingameSaveSlots;

    public SavedGame[] MyIngameSaveSlots
    {
        get
        {
            return ingameSaveSlots;
        }
    }

    private string action;

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private Text titleText;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private Text buttonText;

    private SavedGame current;

    public SavedGame MyCurrentGame
    {
        get
        {
            return current;
        }
    }

    private void Start()
    {

    }

    public void prepareSettings()
    {
        //chests = FindObjectsOfType<Chest>();
        items = InventoryScript.MyInstance.MyItems;
        equipment = FindObjectsOfType<EquipmentButton>();
    }

    public void SelectCharacter(SavedGame savedGame)
    {
        if (current == savedGame)
        {
            current = null;
        }
        else current = savedGame;

        TitleScreenControl.MyInstance.selectCharacter(savedGame.gameObject.GetComponentInChildren<Button>().gameObject);
    }

    public void RefreshSaveSlots(SavedGame[] saveSlots)
    {
        for (int i = 0; i < saveSlots.Length; i++)
            ShowSavedFiles(saveSlots[i]);
    }

    public void ShowDialogue(GameObject clickButton)
    {
        action = clickButton.name;

        switch (action)
        {
            case "Load":
                buttonText.text = "불러오기";
                titleText.text = "불러오기";
                dialogueText.text = "불러오시겠습니까?";
                //Load(clickButton.GetComponentInParent<SavedGame>());
                break;
            case "Save":
                buttonText.text = "저장하기";
                titleText.text = "저장하기";
                dialogueText.text = "저장하시겠습니까?";
                //Save(clickButton.GetComponentInParent<SavedGame>());
                break;
            case "Delete":
                buttonText.text = "삭제하기";
                titleText.text = "삭제하기";
                dialogueText.text = "삭제하시겠습니까?";
                //Delete(clickButton.GetComponentInParent<SavedGame>());
                break;
        }

        current = clickButton.GetComponentInParent<SavedGame>();
        dialogue.SetActive(true);
    }

    public void ExecuteAction()
    {
        switch (action)
        {
            case "Save":
                Save(current);
                break;
            case "Delete":
                Delete(current);
                break;
        }

        CloseDialogue();
    }

    public void LoadScene()
    {
        if (current != null && File.Exists(Application.persistentDataPath + "/" + current.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + current.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            SoundManager.MyInstance.isIngame = true;
            SceneManager.LoadScene(data.MyScene);
            Load(current);
        }
    }

    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    public void ShowSavedFiles(SavedGame savedGame)
    {
        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    public void Delete(SavedGame savedGame)
    {
        File.Delete(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat");
        savedGame.HideInfo();
    }

    public void Save(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Create);

            SaveData data = new SaveData();

            if (SceneManager.GetActiveScene().name == "_preload")
                data.MyScene = "Tutorial";
            else data.MyScene = SceneManager.GetActiveScene().name;

            SaveEquipment(data);

            if (InventoryScript.MyInstance != null)
            {
                SaveBags(data);
                SaveInventory(data);
            }

            SavePlayer(data);

            //SaveChests(data);

            SaveSlotButtons(data);

            SaveQuests(data);

            bf.Serialize(file, data);

            file.Close();

            ShowSavedFiles(savedGame);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void Load(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadEquipment(data);

            LoadBags(data);

            LoadInventory(data);

            LoadPlayer(data);

            //LoadChests(data);

            LoadSlotButtons(data);

            LoadQuests(data);
        }
        catch (System.Exception)
        {
            throw;
            //Delete(savedGame);
            //PlayerPrefs.DeleteKey("Load");
        }
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(Player.MyInstance.MyName, Player.MyInstance.MyClass, Player.MyInstance.MyLevel, Player.MyInstance.MyExp.MyCurrentValue, Player.MyInstance.MyExp.MyMaxValue, 
                                           Player.MyInstance.MyHealth.MyCurrentValue, Player.MyInstance.MyHealth.MyMaxValue, Player.MyInstance.MyMana.MyCurrentValue, Player.MyInstance.MyMana.MyMaxValue, 
                                           Player.MyInstance.gameObject.transform.position, Player.MyInstance.MyStr, Player.MyInstance.MyVit, Player.MyInstance.MyAgi, Player.MyInstance.MyInt, Player.MyInstance.MyStatPoint);
    }

    private void LoadPlayer(SaveData data)
    {
        PlayerData pData = data.MyPlayerData;
        Player.MyInstance.LoadCharacterInfo(pData.MyName, pData.MyClass, pData.MyXp, pData.MyHealth, pData.MyMaxHealth, pData.MyMana, pData.MyMaxMana, pData.MyLevel,
                                            pData.MySTR, pData.MyVIT, pData.MyAGI, pData.MyINTEL, pData.MyBonus, pData.MyX, pData.MyY);
    }

    /*private void SaveChests(SaveData data)
    {
        for (int i = 0; i < chests.Length; i++)
        {
            data.MyChestData.Add(new ChestData(chests[i].name));

            foreach (Item item in chests[i].MyItems)
            {
                if (chests[i].MyItems.Count > 0)
                {
                    data.MyChestData[i].MyItems.Add(new ItemData(item.MyTitle, item.MySlot.MyItems.Count, item.MySlot.MyIndex));
                }
            }
        }
    }

    private void LoadChests(SaveData data)
    {
        foreach (ChestData chest in data.MyChestData)
        {
            Chest c = Array.Find(chests, x => x.name == chest.MyName);

            foreach (ItemData itemData in chest.MyItems)
            {
                Item item = Instantiate(Array.Find(items, x => x.MyTitle == itemData.MyTitle));
                item.MySlot = c.MyBag.MySlots.Find(x => x.MyIndex == itemData.MySlotIndex);
                c.MyItems.Add(item);
            }
        }
    }*/

    private void SaveBags(SaveData data)
    {
        data.MyInventoryData.MyBags.Add(new BagData(InventoryScript.MyInstance.MySlots.Count, 0));
        data.MyInventoryData.MyBags.Add(new BagData(InventoryScript.MyInstance.MySlots2.Count, 1));
        data.MyInventoryData.MyBags.Add(new BagData(InventoryScript.MyInstance.MySlots3.Count, 2));
    }

    private void LoadBags(SaveData data)
    {
        foreach (BagData bagData in data.MyInventoryData.MyBags)
        {

        }
    }

    private void SaveEquipment(SaveData data)
    {
        if (equipment != null)
        {
            foreach (EquipmentButton equipmentButton in equipment)
            {
                if (equipmentButton.MyEquippedArmor != null)
                {
                    data.MyEquipmentData.Add(new EquipmentData(equipmentButton.MyEquippedArmor.MyTitle, equipmentButton.name));
                }
            }
        }
    }

    private void LoadEquipment(SaveData data)
    {
        foreach (EquipmentData equipmentData in data.MyEquipmentData)
        {
            EquipmentButton eb = Array.Find(equipment, x => x.name == equipmentData.MyType);

            eb.EquipArmor(Array.Find(items, x => x.MyTitle == equipmentData.MyTitle) as Armor);
        }
    }

    private void SaveSlotButtons(SaveData data)
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            if (slotButtons[i].MyUsable != null)
            {
                SlotButtonData s;

                if (slotButtons[i].MyUsable is Spell)
                {
                    s = new SlotButtonData((slotButtons[i].MyUsable as Spell).MyName, false, i);
                }
                else
                {
                    s = new SlotButtonData((slotButtons[i].MyUsable as Item).MyTitle, true, i);
                }

                data.MySlotButtonData.Add(s);
            }
        }
    }

    private void LoadSlotButtons(SaveData data)
    {
        foreach (SlotButtonData buttonData in data.MySlotButtonData)
        {
            if (buttonData.IsItem)
            {
                slotButtons[buttonData.MyIndex].SetUsable(InventoryScript.MyInstance.GetUsables(buttonData.MyAction));
            }
            else
            {
                slotButtons[buttonData.MyIndex].SetUsable(SpellBook.MyInstance.GetSpell(buttonData.MyAction));
            }
        }
    }

    private void SaveInventory(SaveData data)
    {
        List<SlotScript> slots = InventoryScript.MyInstance.GetAllItems();

        foreach (SlotScript slot in slots)
        {
            data.MyInventoryData.MyItems.Add(new ItemData(slot.MyItem.MyTitle, slot.MyItems.Count, slot.MyIndex, slot.MyBagIndex));
        }
    }

    private void LoadInventory(SaveData data)
    {
        foreach (ItemData itemData in data.MyInventoryData.MyItems)
        {
            Item item = Instantiate(Array.Find(items, x => x.MyTitle == itemData.MyTitle));

            for (int i = 0; i < itemData.MyStackCount; i++)
            {
                InventoryScript.MyInstance.PlaceInSpecific(item, itemData.MySlotIndex, itemData.MyBagIndex);
            }
        }
    }

    private void SaveQuests(SaveData data)
    {
        foreach (Quest quest in QuestManager.MyInstance.MyQuests)
        {
            data.MyQuestData.Add(new QuestData(quest.MyTitle, quest.MyCollectObjectives, quest.MyKillObjectives, quest.IsAccepted, quest.IsCompleted, quest.messagePopuped));
        }
    }

    private void LoadQuests(SaveData data)
    {
        for (int i = 0; i < QuestManager.MyInstance.MyQuests.Length; i++)
        {
            for (int j = 0; j < QuestManager.MyInstance.MyQuests[i].MyCollectObjectives.Length; j++)
            {
                QuestManager.MyInstance.MyQuests[i].MyCollectObjectives[j].MyCurrentAmount = data.MyQuestData[i].MyCollectObjectives[j].MyCurrentAmount;
            }

            for (int j = 0; j < QuestManager.MyInstance.MyQuests[i].MyKillObjectives.Length; j++)
            {
                QuestManager.MyInstance.MyQuests[i].MyKillObjectives[j].MyCurrentAmount = data.MyQuestData[i].MyKillObjectives[j].MyCurrentAmount;
            }

            QuestManager.MyInstance.MyQuests[i].IsAccepted = data.MyQuestData[i].IsAccepted;
            QuestManager.MyInstance.MyQuests[i].IsCompleted = data.MyQuestData[i].IsCompleted;
            QuestManager.MyInstance.MyQuests[i].messagePopuped = data.MyQuestData[i].messagePopuped;

            if (QuestManager.MyInstance.MyQuests[i].IsAccepted && !QuestManager.MyInstance.MyQuests[i].IsCompleted)
            {
                Questlog.MyInstance.AcceptQuest(QuestManager.MyInstance.MyQuests[i]);
            }
        }
    }

    public int GetCurrentIndex(SavedGame[] saveSlots)
    {
        int index = 0;
        foreach (SavedGame savedGame in saveSlots)
        {
            if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
            {
                index++;
            }
        }

        return index;
    }

    public bool checkSaveDataFile(int index, SavedGame[] saveSlots)
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveSlots[index].gameObject.name + ".dat"))
        {
            return true;
        }

        return false;
    }
}
