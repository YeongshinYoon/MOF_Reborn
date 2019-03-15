using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLootTable : LootTable, IInteractable 
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite gatherSprite;

    private void Start()
    {
        RollLoot();
    }

    protected override void RollLoot()
    {
        foreach (Loot l in loots)
        {
            int roll = Random.Range(0, 100);

            if (roll <= l.MyDropChance)
            {
                int itemCount = Random.Range(1, 6);

                for (int i = 0; i < itemCount; i++)
                {
                    droppedItems.Add(l.MyItem);
                }

                spriteRenderer.sprite = gatherSprite;
            }
            else gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        Player.MyInstance.Gather("Gathering...");
    }

    public void StopInteract()
    {
        spriteRenderer.sprite = defaultSprite;
        gameObject.SetActive(false);
    }
}
