using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorWindowBuy : MonoBehaviour 
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private VendorBuyButton[] vendorButtons;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

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
        basketText.text = "바구니 (" + InventoryScript.MyInstance.MyFullBuyBasketCount + "/20)";
        totalRibi.text = total + "";
        holdingRibi.text = InventoryScript.MyInstance.MyRibi + "";
    }

    public void CreatePages(VendorItem[] items)
    {
        pages.Clear();
        List<VendorItem> page = new List<VendorItem>();

        for (int i = 0; i < items.Length; i++)
        {
            page.Add(items[i]);

            if (page.Count == 9 || i == items.Length - 1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
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

    public void AddToBasket(VendorItem item)
    {
        InventoryScript.MyInstance.AddToBuyBasket(item.item);
        total += item.item.MyPrice;
    }

    public void Open()
    {
        VendorBuy.MyInstance.SetCurrentItems(DialogueBox.MyInstance.currentNPC.MyNPCName);
        CreatePages(VendorBuy.MyInstance.MyCurrentItems);

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close() 
    {
        InventoryScript.MyInstance.ClearBuyBasket();
        total = 0;
       
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
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

    public void ClearButtons()
    {
        foreach (VendorBuyButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }
}
