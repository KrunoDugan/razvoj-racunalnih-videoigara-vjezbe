using UnityEngine;

public class Archer : Character
{
    public Archer(string name) : base(name, 90, 18) { }

    public override void Attack(Character target)
    {
        int crit = Random.Range(0, 2);

        if (crit == 1)
        {
            Debug.Log(Name + " lands CRITICAL shot!");
            target.TakeDamage((AttackPower + attackBuff) * 2);
        }
        else
        {
            base.Attack(target);
        }
    }
}