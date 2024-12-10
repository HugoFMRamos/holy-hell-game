using UnityEngine;

public class PlayerSystem : MonoBehaviour {
    
    [Header("Player Stats")]
    public int health = 100;
    public int maxHealth = 200;
    public int armor = 100;
    public int maxArmor = 200;
    public float armorAbsorbtionPercentage = .8f;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            DamageMe(20);
        }
    }

    public void DamageMe(int damage) {
        if (armor == 0) {
            health -= damage;
            if (health < 0) {
                health = 0;
            }
            return;
        }

        int damageToArmor = (int)(damage * armorAbsorbtionPercentage);
        int damageToHealth = damage - damageToArmor;

        armor -= damageToArmor;
        if (armor < 0) {
            armor = 0;
        }

        health -= damageToHealth;
        if (health < 0) {
            health = 0;
        }
    }

    public void HealMe(int valueToAdd, bool addToHealth) {
        if(addToHealth) {
            health += valueToAdd;
            if(health > maxHealth) {
                health = maxHealth;
            }
        }
        else {
            armor += valueToAdd;
            if(armor > maxArmor) {
                armor = maxArmor;
            }
        }
    }
}