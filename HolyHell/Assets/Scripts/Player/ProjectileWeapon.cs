using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Specific Weapon Stats")]
    public GameObject projectilePrefab;
    private float timer;

    public AudioWeaponCall audioWeaponCall;

    private void Start()
    {
        timer = 0f;
        base.ammo = base.startAmmo;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            weaponAnimator.SetBool("Idle", false);
            weaponAnimator.SetBool("Walk", true);
        } else {
            weaponAnimator.SetBool("Idle", true);
            weaponAnimator.SetBool("Walk", false);
        }

        if(Input.GetKey(fireButton) && base.ammo != 0) {
            weaponAnimator.SetBool("Attack", true);
            weaponAnimator.SetBool("Walk", false);
            weaponAnimator.SetBool("Idle", false);
        } else {
            weaponAnimator.SetBool("Attack", false);
        }

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

        if(audioWeaponCall != null){
            audioWeaponCall.FireRandom();
        }
    }
}
