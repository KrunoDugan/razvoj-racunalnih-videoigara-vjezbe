using UnityEngine;

public class StrengthPotion : Item
{
    public StrengthPotion()
    {
        Name = "Strength Potion";
    }

    public override void Use(Character character)
    {
        Debug.Log(character.Name + " drinks Strength Potion!");
        character.AddAttackBuff(10, 3);
    }
}