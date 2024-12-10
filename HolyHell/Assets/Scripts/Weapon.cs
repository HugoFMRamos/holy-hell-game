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
    
    public virtual void Fire() {}
}
