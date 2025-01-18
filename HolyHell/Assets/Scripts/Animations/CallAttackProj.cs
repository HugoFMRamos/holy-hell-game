using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAttackProj : MonoBehaviour
{
    public EnemyTypeProjectile enemy;

    public void callAttack() {
        enemy.attackCalled();
    }

    public void Reset() {
        enemy.ResetAttack();
    }
}
