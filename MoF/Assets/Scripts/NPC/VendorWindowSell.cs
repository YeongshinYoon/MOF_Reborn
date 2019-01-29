using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorWindowSell : MonoBehaviour 
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private VendorSellButton[] vendorButtons;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    private int bagIndex;

    private int pageIndex;

    [SerializeField]
    private Text pageNumber;

    private int total = 0;

    [SerializeField]
    private Text totalRibi;

    [SerializeField]
    private Text holdingRibi;

    [SerializeField]
    private Text basketText;


    void Update()
    {
        basketText.text = "바구니 (" + InventoryScript.MyInstance.MyFullSellBasketCount + "/20)";
        totalRibi.text = total + "";
        holdingRibi.text = InventoryScript.MyInstance.MyRibi + "";
    }

    public void CreatePages(VendorItem[] items)
    {
        pages.Clear();
        List<VendorItem> page = new List<VendorItem>();

        if (items != null)
        {

            for (int i = 0; i < items.Length; i++)
            {
                page.Add(items[i]);

                if (page.Count == 9 || i == items.Length - 1)
                {
                    pages.Add(page);
                    page = new List<VendorItem>();
                }
            }

        }
        AddItems();
    }

    public void AddItems()
    {
        pageNumber.text = (pageIndex + 1) + " / " + pages.Count;

        if (pages.Count > 0)
        {
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0)
        {
            ClearButtons();
            pageIndex--;
            AddItems();
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count - 1)
        {
            ClearButtons();
            pageIndex++;
            AddItems();
        }
    }

    public void AddToBasket(VendorItem item)
    {
        InventoryScript.MyInstance.AddToSellBasket(item.item);
        total += item.item.MyPrice;
        item.stack--;
    }

    public void ClearButtons()
    {
        foreach (VendorSellButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void Bag1Page()
    {
        ClearButtons();
        VendorSell.MyInstance.items_bag1();
        CreatePages(VendorSell.MyInstance.MyItems);
    }

    public void Bag2Page()
    {
        ClearButtons();
        VendorSell.MyInstance.items_bag2();
        CreatePages(VendorSell.MyInstance.MyItems);
    }

    public void Bag3Page()
    {
        ClearButtons();
        VendorSell.MyInstance.items_bag3();
        CreatePages(VendorSell.MyInstance.MyItems);
    }

    public void Open()
    {
        Bag1Page();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        InventoryScript.MyInstance.ClearSellBasket();
        total = 0;

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
