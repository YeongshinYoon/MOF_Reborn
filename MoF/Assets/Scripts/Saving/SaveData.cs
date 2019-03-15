using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    public PlayerData MyPlayerData { get; set; }

    public List<ChestData> MyChestData { get; set; }

    public List<EquipmentData> MyEquipmentData { get; set; }

    public InventoryData MyInventoryData { get; set; }

    public List<SlotButtonData> MySlotButtonData { get; set; }

    public List<QuestData> MyQuestData { get; set; }

    public DateTime MyDateTime { get; set; }

    public string MyScene { get; set; }

    public SaveData()
    {
        MyInventoryData = new InventoryData();
        MyChestData = new List<ChestData>();
        MyEquipmentData = new List<EquipmentData>();
        MySlotButtonData = new List<SlotButtonData>();
        MyQuestData = new List<QuestData>();
        MyDateTime = DateTime.Now;
    }
}

[Serializable]
public class PlayerData
{
    public string MyName { get; set; }

    public string MyClass { get; set; }

    public int MyLevel { get; set; }

    public float MyXp { get; set; }

    public float MyMaxXp { get; set; }

    public float MyHealth { get; set; }

    public float MyMaxHealth { get; set; }

    public float MyMana { get; set; }

    public float MyMaxMana { get; set; }

    public float MyX { get; set; }

    public float MyY { get; set; }

    public int MySTR { get; set; }

    public int MyVIT { get; set; }

    public int MyAGI { get; set; }

    public int MyINTEL { get; set; }

    public int MyBonus { get; set; }

    public PlayerData (string name, string Class, int level, float xp, float maxXp, float health, float maxHealth, float mana, float maxMana, Vector2 position, int str, int vit, int agi, int intel, int bonus)
    {
        MyName = name;
        MyClass = Class;
        MyLevel = level;
        MyXp = xp;
        MyMaxXp = maxXp;
        MyHealth = health;
        MyMaxHealth = maxHealth;
        MyMana = mana;
        MyMaxMana = maxMana;
        MyX = position.x;
        MyY = position.y;
        MySTR = str;
        MyVIT = vit;
        MyAGI = agi;
        MyINTEL = intel;
        MyBonus = bonus;
    }
}

[Serializable]
public class ItemData
{
    public string MyTitle { get; set; }

    public int MyStackCount { get; set; }

    public int MyBagIndex { get; set; }

    public int MySlotIndex { get; set; }

    public ItemData (string title, int stackCount = 0, int slotIndex = 0, int bagIndex = 0)
    {
        MyTitle = title;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
        MyBagIndex = bagIndex;
    }
}

[Serializable]
public class ChestData
{
    public string MyName { get; set; }

    public List<ItemData> MyItems { get; set; }

    public ChestData (string name)
    {
        MyName = name;

        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class InventoryData
{
    public List<BagData> MyBags { get; set; }

    public List<ItemData> MyItems { get; set; }

    public InventoryData()
    {
        MyBags = new List<BagData>();
        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class BagData
{
    public int MySlotCount { get; set; }
    public int MyBagIndex { get; set; }

    public BagData (int count, int index)
    {
        MySlotCount = count;
        MyBagIndex = index;
    }
}

[Serializable]
public class EquipmentData
{
    public string MyTitle { get; set; }

    public string MyType { get; set; }

    public EquipmentData (string title, string type)
    {
        MyTitle = title;
        MyType = type;
    }
}

[Serializable]
public class SlotButtonData
{
    public string MyAction { get; set; }

    public bool IsItem { get; set; }

    public int MyIndex { get; set; }

    public SlotButtonData(string action, bool isItem, int index)
    {
        MyAction = action;
        IsItem = isItem;
        MyIndex = index;
    }
}

[Serializable]
public class QuestData
{
    public string MyTitle { get; set; }

    public CollectObjective[] MyCollectObjectives { get; set; }

    public KillObjective[] MyKillObjectives { get; set; }

    public bool IsAccepted;

    public bool IsCompleted;

    public bool messagePopuped;

    public QuestData (string title, CollectObjective[] collectObjectives, KillObjective[] killObjectives, bool isAccepted, bool isCompleted, bool messagePopuped)
    {
        MyTitle = title;
        MyCollectObjectives = collectObjectives;
        MyKillObjectives = killObjectives;
        IsAccepted = isAccepted;
        IsCompleted = isCompleted;
        this.messagePopuped = messagePopuped;
    }
}