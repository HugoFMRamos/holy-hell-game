using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("General Weapon Stats")]
    public float firerate;
    public Transform weaponTransform;
    public KeyCode fireButton;

    public virtual void Fire() {}
}
