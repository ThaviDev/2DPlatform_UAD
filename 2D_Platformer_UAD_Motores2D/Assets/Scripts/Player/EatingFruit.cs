using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EatingFruit : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var fruit = collision.GetComponent<FruitBehaviour>();
        var spike = collision.GetComponent<SpikeScript>();
        var finishFlag = collision.GetComponent<FinishFlagScript>();
        if (fruit != null)
        {
            _player.HasColectedFruit();
            Destroy(collision.gameObject);
        }
        if (spike != null)
        {
            _player.Damage(1);
        }
        if (finishFlag != null)
        {
            _player.PlayerWon();
        }
    }
}