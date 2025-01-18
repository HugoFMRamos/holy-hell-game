using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAttack : MonoBehaviour
{
    public EnemyTypeMelee enemy;

    public void callAttack() {
        enemy.attackCalled();
    }

    public void Reset() {
        enemy.ResetAttack();
    }
}
