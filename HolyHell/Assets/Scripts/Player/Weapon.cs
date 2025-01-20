using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("General Weapon Stats")]
    public float firerate;
    public int startAmmo;
    public int maxAmmo;
    public int ammo;
    public bool isInInventory;
    public Transform weaponTransform;
    public KeyCode fireButton;
    public LayerMask whatIsEnemy;
    
    public virtual void Fire() {}

    public void TriggerEnemies() {
        Collider[] enemiesTriggered = Physics.OverlapSphere(transform.position, 0f, whatIsEnemy);

        foreach(Collider collider in enemiesTriggered) {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.detectedWeaponSound = true;
        }
    }
}
