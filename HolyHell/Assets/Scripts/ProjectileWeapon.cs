using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Specific Weapon Stats")]
    public GameObject projectilePrefab;
    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(Input.GetKey(fireButton) && timer < 0f) {
            Fire();
            timer = base.firerate;
        }
    }

    public override void Fire() {
        Instantiate(projectilePrefab, weaponTransform.position, weaponTransform.rotation);
    }
}
