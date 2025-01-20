using UnityEngine;

public class HitscanWeapon : Weapon
{
    [Header("Specific Weapon Stats")]
    public int damage;
    public int numberOfRays;
    public float range;
    public float spread;
    public LayerMask whatToHit;
    private float timer;
    private RaycastHit raycastHit;

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
        for(int i = 0; i < numberOfRays; i++) {
            Vector3 direction = base.weaponTransform.forward +
                                new Vector3(
                                    Random.Range(-spread, spread),
                                    Random.Range(-spread, spread),
                                    0f);

            if(Physics.Raycast(base.weaponTransform.position, direction, out raycastHit, range, whatToHit)) {
                Enemy enemy = raycastHit.collider.gameObject.GetComponent<Enemy>();
                if(enemy != null) {
                    enemy.TakeDamage(damage);
                }

                Debug.DrawLine(base.weaponTransform.position, raycastHit.point, Color.green, 2.5f);
                Debug.Log("Did Hit! Damage: " + damage);
            }
            else { 
                Debug.DrawLine(base.weaponTransform.position, direction * range, Color.red, 2.5f);
                Debug.Log("Did not hit!");
            }   
        }
    }
}
