using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Warrior warrior = new Warrior("Thor");
        Mage mage = new Mage("Merlin");
        Archer archer = new Archer("Legolas");

        warrior.Inventory.AddItem(new HealthPotion(30));
        warrior.Inventory.AddItem(new StrengthPotion());
        warrior.Inventory.AddItem(new ShieldPotion());

        Debug.Log("=== BATTLE START ===");

        warrior.Inventory.UseItem(1, warrior); // strength buff
        warrior.Attack(mage);

        mage.Attack(warrior);

        warrior.Inventory.UseItem(2, warrior); // defense buff

        mage.Attack(warrior);

        warrior.Inventory.UseItem(0, warrior); // heal

        warrior.Attack(mage);

        Debug.Log("=== SECOND BATTLE ===");

        archer.Attack(mage);
        mage.Attack(archer);
        archer.Attack(mage);

        Debug.Log("=== BATTLE END ===");
    }
}