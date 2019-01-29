using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Buy
public class VendorBuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text price;

    private VendorWindowBuy vendorWindowBuy;

    private VendorItem vendorItem;

    void Start()
    {
        vendorWindowBuy = FindObjectOfType<VendorWindowBuy>();
    }

    public void AddItem(VendorItem vendorItem) 
    {
        this.vendorItem = vendorItem;

        icon.sprite = vendorItem.item.MyIcon;
        title.text = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[vendorItem.item.MyQuality], vendorItem.item.MyTitle);

        if (vendorItem.item.MyPrice > 0)
        {
            price.text = vendorItem.item.MyPrice.ToString() + "리비";    
        }
        else 
        {
            price.text = string.Empty;
        }

        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Add To Basket
        vendorWindowBuy.AddToBasket(vendorItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(new Vector2(0, 1), transform.position, vendorItem.item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();   
    }
}
