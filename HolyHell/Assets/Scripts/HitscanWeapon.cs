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

        for(int i = 0; i < numberOfRays; i++) {
            Vector3 direction = base.weaponTransform.forward +
                                new Vector3(
                                    Random.Range(-spread, spread),
                                    Random.Range(-spread, spread),
                                    0f);

            if(Physics.Raycast(base.weaponTransform.position, direction, out raycastHit, range, whatToHit)) {
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
