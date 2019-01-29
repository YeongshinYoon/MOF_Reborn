using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBindManager : MonoBehaviour {
    private static KeyBindManager instance;

    public static KeyBindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }

            return instance;
        }
    }
    
    public Dictionary<string, KeyCode> KeyBinds { get; private set; }

    public Dictionary<string, KeyCode> SlotBinds { get; private set; }

    private string bindName;

    // Use this for initialization
    void Start () {
        KeyBinds = new Dictionary<string, KeyCode>();

        SlotBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.UpArrow);
        BindKey("DOWN", KeyCode.DownArrow);
        BindKey("LEFT", KeyCode.LeftArrow);
        BindKey("RIGHT", KeyCode.RightArrow);

        BindKey("MENU", KeyCode.Escape);
        BindKey("HELP", KeyCode.F1);
        BindKey("KEYBINDS", KeyCode.F2);
        BindKey("SCREENSHOT", KeyCode.Print);

        BindKey("SLOT1", KeyCode.Alpha1);
        BindKey("SLOT2", KeyCode.Alpha2);
        BindKey("SLOT3", KeyCode.Alpha3);
        BindKey("SLOT4", KeyCode.Alpha4);
        BindKey("SLOT5", KeyCode.Alpha5);
        BindKey("SLOT6", KeyCode.Alpha6);
        BindKey("SLOT7", KeyCode.Alpha7);
        BindKey("SLOT8", KeyCode.Alpha8);
        BindKey("SLOT9", KeyCode.Alpha9);

        BindKey("QUEST", KeyCode.Q);
        BindKey("WORLDMAP", KeyCode.W);
        BindKey("EQUIPMENT", KeyCode.E);
        BindKey("INVENTORY", KeyCode.I);
        BindKey("OPTION", KeyCode.O);
        BindKey("SKILL", KeyCode.S);
        BindKey("DIARY", KeyCode.D);
        BindKey("GETITEM", KeyCode.Z);
        BindKey("CHARACTER_INFORMATION", KeyCode.C);
        BindKey("MINIMAP", KeyCode.M);
        BindKey("ATTACK", KeyCode.LeftControl);
    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        if (key.Contains("SLOT"))
        {
            currentDictionary = SlotBinds;
        }
        if (!currentDictionary.ContainsValue(keyBind))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictionary.ContainsKey(key))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;

            currentDictionary[myKey] = KeyCode.None;

            UIManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                BindKey(bindName, e.keyCode);
            }
        }
    }
}
