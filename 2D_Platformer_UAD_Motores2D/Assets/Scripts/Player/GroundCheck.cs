using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.Grounded();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _player.Ungrounded();
    }
}
