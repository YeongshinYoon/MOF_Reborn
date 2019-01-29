using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    private static LootWindow instance;

    public static LootWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LootWindow>();
            }

            return instance;
        }
    }

    private List<List<Item>> pages = new List<List<Item>>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private LootButton[] lootButtons;

    [SerializeField]
    private Item[] items;

    [SerializeField]
    private GameObject nextBtn, prevBtn;

    private CanvasGroup canvasGroup;

    private List<Item> droppedLoot = new List<Item>();

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatePages(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 12 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }

            AddLoot();

            Open();
        }
    }

    private void AddLoot()
    {
        if (pages.Count > 0)
        {
            pageNumber.text = (pageIndex + 1) + " / " + pages.Count;
            prevBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            for (int itemIndex = 0; itemIndex < pages[pageIndex].Count; itemIndex++)
            {
                if (pages[pageIndex][itemIndex] != null)
                {
                    lootButtons[itemIndex].MyIcon.sprite = pages[pageIndex][itemIndex].MyIcon;

                    lootButtons[itemIndex].MyLoot = pages[pageIndex][itemIndex];

                    lootButtons[itemIndex].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][itemIndex].MyQuality], pages[pageIndex][itemIndex].MyTitle);

                    lootButtons[itemIndex].MyTitle.text = title;
                }
            }
        }
    }

    public void ClearButtons()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count - 1)
        {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);

        droppedLoot.Remove(loot);

        if (pages[pageIndex].Count == 0)
        {
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }

            AddLoot();
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();
    }
}
