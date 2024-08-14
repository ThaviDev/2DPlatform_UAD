using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor : MonoBehaviour
{
    [SerializeField] int _myHealth;
    public void GetDamaged()
    {
        _myHealth -= 1;
        if (_myHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
