using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Specific Weapon Stats")]
    public GameObject projectilePrefab;
    private float timer;
    public bool fireSpecificSFX;

    AudioWeaponCall audioWeaponCall;

    private void Start()
    {
        timer = 0f;
        base.ammo = base.startAmmo;

        audioWeaponCall = GetComponent<AudioWeaponCall>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(Input.GetKey(fireButton) && timer < 0f && base.ammo != 0) {
            Fire();
            timer = base.firerate;
            base.ammo -= 1;
        }
    }

    public override void Fire() {
        TriggerEnemies();
        Instantiate(projectilePrefab, weaponTransform.position, weaponTransform.rotation);

        if(!fireSpecificSFX) audioWeaponCall.FireRandom();
        else audioWeaponCall.FireSpecific(0);

    }
}
