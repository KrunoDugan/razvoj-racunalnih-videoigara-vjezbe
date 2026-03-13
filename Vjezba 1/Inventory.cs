using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items = new List<Item>();
    private int maxSlots;

    public Inventory(int maxSlots)
    {
        this.maxSlots = maxSlots;
    }

    public void AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory full!");
            return;
        }

        items.Add(item);
        Debug.Log(item.Name + " added to inventory.");
    }

    public void UseItem(int index, Character character)
    {
        if (index < 0 || index >= items.Count)
            return;

        items[index].Use(character);
        items.RemoveAt(index);
    }
}