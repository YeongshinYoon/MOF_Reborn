using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup[] menus;

    [SerializeField]
    private SlotButton[] slotButtons;

    [SerializeField]
    private GameObject targetFrame;

    private Stat healthStat;

    [SerializeField]
    private Text targetName;

    [SerializeField]
    private Text targetLevel;

    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup spellBook;

    private GameObject[] keyBindButtons;

    [SerializeField]
    private GameObject tooltip;

    //[SerializeField]
    private RectTransform tooltipRect;

    private Text tooltipText;

    [SerializeField]
    private Equipment equipment;

    [SerializeField]
    private CharacterInformation charinfo;

    private void Awake()
    {
        keyBindButtons = GameObject.FindGameObjectsWithTag("KeyBind");
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start()
    {
        tooltipRect = tooltip.GetComponent<RectTransform>();
        healthStat = targetFrame.GetComponentInChildren<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (preload.MyInstance.MyUICanvas.activeSelf)
        {
            //ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenClose(menus[0]);
            }
            //Inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenClose(menus[1]);
            }
            //SpellBook
            if (Input.GetKeyDown(KeyCode.S))
            {
                OpenClose(menus[2]);
            }
            //Equipment
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenClose(menus[3]);
            }
            //Questlog
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OpenClose(menus[4]);
            }
            //Character Information
            if (Input.GetKeyDown(KeyCode.C))
            {
                OpenClose(menus[5]);
            }
        }
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    OpenClose(keybindMenu);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    OpenClose(spellBook);
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    equipment.OpenClose();
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    charinfo.OpenClose();
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Questlog.MyInstance.OpenClose();
        //}
    }

    public void OpenSingle(CanvasGroup canvasGroup)
    {
        foreach (CanvasGroup canvas in menus)
        {
            CloseSingle(canvas);
        }

        if (canvasGroup.gameObject.name == "SaveGame")
        {
            SaveManager.MyInstance.RefreshSaveSlots(SaveManager.MyInstance.MyIngameSaveSlots);
        }

        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void CloseSingle(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void showTargetFrame(Enemy target)
    {
        targetFrame.SetActive(true);

        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);
        targetName.text = target.MyName;
        targetLevel.text = target.MyLevel.ToString();

        target.healthChanged += new HealthChanged(updateTargetFrame);

        target.characterRemoved += new CharacterRemoved(hideTargetFrame);

        if (target.MyLevel >= Player.MyInstance.MyLevel + 5)
        {
            targetName.color = Color.red;
        }
        else if ((target.MyLevel == Player.MyInstance.MyLevel + 3) || (target.MyLevel == Player.MyInstance.MyLevel + 4))
        {
            targetName.color = new Color32(255, 124, 0, 255);
        }
        else if ((target.MyLevel >= Player.MyInstance.MyLevel - 2) && (target.MyLevel <= Player.MyInstance.MyLevel + 2))
        {
            targetName.color = Color.yellow;
        }
        else if (target.MyLevel <= Player.MyInstance.MyLevel - 3 && target.MyLevel > XPManager.CalculateGrayLevel())
        {
            targetName.color = Color.green;
        }
        else targetName.color = Color.gray;
    }

    public void hideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void updateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keyBindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickSlotButton(string buttonName)
    {
        Array.Find(slotButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackSize.text = clickable.MyCount.ToString();
            clickable.MyStackSize.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackSize.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackSize.color = new Color(0, 0, 0, 0);
        }
    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackSize.color = new Color(0, 0, 0, 0);
        clickable.MyIcon.color = Color.white;
    }

    public void ShowTooltip(Vector3 position, IDescribable description)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }

    public void ShowTooltip(Vector2 pivot, Vector3 position, IDescribable description)
    {
        tooltipRect.pivot = pivot;
        ShowTooltip(position, description);
    }

    public void HideTooltip()
    {
        //tooltipRect.pivot = new Vector2(1, 0);
        tooltip.SetActive(false);
    }

    public void RefreshToolTip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
