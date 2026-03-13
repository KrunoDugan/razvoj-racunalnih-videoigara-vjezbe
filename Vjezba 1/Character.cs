using UnityEngine;

public abstract class Character
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int AttackPower { get; protected set; }

    protected int attackBuff = 0;
    protected int defenseBuff = 0;
    protected int buffTurns = 0;

    public Inventory Inventory { get; private set; }

    public Character(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        AttackPower = attack;

        Inventory = new Inventory(5);
    }

    public virtual void Attack(Character target)
    {
        int totalAttack = AttackPower + attackBuff;

        Debug.Log(Name + " attacks " + target.Name);
        target.TakeDamage(totalAttack);

        UpdateBuff();
    }

    public virtual void TakeDamage(int dmg)
    {
        int finalDamage = dmg - defenseBuff;

        if (finalDamage < 0)
            finalDamage = 0;

        Health -= finalDamage;

        if (Health < 0)
            Health = 0;

        Debug.Log(Name + " takes " + finalDamage + " damage. HP: " + Health);

        UpdateBuff();
    }

    public void Heal(int amount)
    {
        Health += amount;
        Debug.Log(Name + " heals " + amount + " HP. Total HP: " + Health);
    }

    public void AddAttackBuff(int amount, int turns)
    {
        attackBuff = amount;
        buffTurns = turns;

        Debug.Log(Name + " gains +" + amount + " attack for " + turns + " turns!");
    }

    public void AddDefenseBuff(int amount, int turns)
    {
        defenseBuff = amount;
        buffTurns = turns;

        Debug.Log(Name + " gains +" + amount + " defense for " + turns + " turns!");
    }

    void UpdateBuff()
    {
        if (buffTurns > 0)
        {
            buffTurns--;

            if (buffTurns == 0)
            {
                attackBuff = 0;
                defenseBuff = 0;
                Debug.Log(Name + "'s buffs expired.");
            }
        }
    }
}
