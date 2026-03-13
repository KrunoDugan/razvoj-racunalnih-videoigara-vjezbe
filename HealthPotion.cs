using UnityEngine;

public class HealthPotion : Item
{
    int healAmount;

    public HealthPotion(int amount)
    {
        Name = "Health Potion";
        healAmount = amount;
    }

    public override void Use(Character character)
    {
        Debug.Log(character.Name + " drinks Health Potion!");
        character.Heal(healAmount);
    }
}