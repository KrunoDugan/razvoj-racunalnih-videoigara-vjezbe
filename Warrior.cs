using UnityEngine;

public class Warrior : Character
{
    public Warrior(string name) : base(name, 120, 20) { }

    public override void Attack(Character target)
    {
        Debug.Log(Name + " performs HEAVY sword attack!");
        target.TakeDamage(AttackPower + 5 + attackBuff);
    }
}