using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LootButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private Image icon;
    public Image MyIcon
    {
        get
        {
            return icon;
        }
    }

    [SerializeField]
    private Text title;
    public Text MyTitle
    {
        get
        {
            return title;
        }
    }

    private LootWindow lootWindow;

    public Item MyLoot { get; set; }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(transform.position, MyLoot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(InventoryScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UIManager.MyInstance.HideTooltip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
