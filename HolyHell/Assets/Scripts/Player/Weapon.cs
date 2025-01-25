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
    public Animator weaponAnimator;
    public PlayerSystem playerSystem;
    
    public virtual void Fire() {}
}
