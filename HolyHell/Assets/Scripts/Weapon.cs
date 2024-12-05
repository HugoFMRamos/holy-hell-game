using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("General Weapon Stats")]
    public float firerate;
    public int ammo;
    public AmmoType ammoType;
    public enum AmmoType {
        wafers,
        shotgunShells,
        grenades,
        scepter,
        bible,
    }
    public Transform weaponTransform;
    public KeyCode fireButton;

    public virtual void Fire() {}
}
