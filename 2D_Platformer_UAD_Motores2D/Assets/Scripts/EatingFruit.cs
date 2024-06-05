using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingFruit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var fruit = collision.GetComponent<FruitBehaviour>();
        if (fruit != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
