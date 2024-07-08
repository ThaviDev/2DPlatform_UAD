using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EatingFruit : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var fruit = collision.GetComponent<FruitBehaviour>();
        if (fruit != null)
        {
            _player.HasColectedFruit();
            Destroy(collision.gameObject);
        }
    }
}
