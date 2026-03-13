using UnityEngine;

public class ShieldPotion : Item
{
    public ShieldPotion()
    {
        Name = "Shield Potion";
    }

    public override void Use(Character character)
    {
        Debug.Log(character.Name + " uses Shield Potion!");
        character.AddDefenseBuff(5, 3);
    }
}