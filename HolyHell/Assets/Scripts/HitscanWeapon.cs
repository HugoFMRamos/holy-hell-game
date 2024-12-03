using UnityEngine;

public class HitscanWeapon : Weapon
{
    [Header("Specific Weapon Stats")]
    public int damage;
    public float range;
    public LayerMask whatToHit;
    private float timer;
    private RaycastHit raycastHit;

    private void Start()
    {
        timer = base.firerate;
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
        if(Physics.Raycast(base.weaponTransform.position, base.weaponTransform.forward, out raycastHit, range, whatToHit)) {
            Debug.DrawLine(base.weaponTransform.position, raycastHit.point, Color.green, 2.5f);
            Debug.Log("Did Hit! Damage: " + damage);
        }
        else { 
            Debug.DrawLine(base.weaponTransform.position, base.weaponTransform.forward * range, Color.red, 2.5f);
            Debug.Log("Did not hit!");
        }
    }
}
