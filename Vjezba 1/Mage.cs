using UnityEngine;

public class Mage : Character
{
    private int mana = 50;

    public Mage(string name) : base(name, 80, 15) { }

    public override void Attack(Character target)
    {
        if (mana >= 10)
        {
            Debug.Log(Name + " casts FIREBALL!");
            target.TakeDamage(AttackPower + 10 + attackBuff);
            mana -= 10;
        }
        else
        {
            Debug.Log(Name + " has no mana! Weak attack.");
            base.Attack(target);
        }
    }
}