using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour 
{
    [SerializeField]
    protected Loot[] loots;

    [SerializeField]
    private int[] ribi = new int[2];

    protected List<Item> droppedItems = new List<Item>();

    private bool rolled = false;

	public void ShowLoot()
    {
        if (!rolled)
        {
            RollLoot();
        }

        foreach (Item item in droppedItems)
        {
            InventoryScript.MyInstance.AddItem(item);
        }

        InventoryScript.MyInstance.gainRibi(Random.Range(ribi[0], ribi[1]));
        //LootWindow.MyInstance.CreatePages(droppedItems);
    }

    protected virtual void RollLoot()
    {
        foreach(Loot loot in loots)
        {
            int roll = Random.Range(0, 100);

            if (roll <= loot.MyDropChance)
            {
                droppedItems.Add(loot.MyItem);
            }
        }

        rolled = true;
    }
}
