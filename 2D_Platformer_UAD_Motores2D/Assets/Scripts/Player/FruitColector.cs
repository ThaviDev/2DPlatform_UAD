using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitColector : MonoBehaviour
{
    [SerializeField] int _fruitAmount;
    [SerializeField] int _fruitColected;
    [SerializeField] int _fruitToHeal;

    [SerializeField] PlayerMovement _player;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
    }

    public void AddFruit()
    {
        _fruitAmount += 1;
        CheckConditionToHeal();
    }

    public int GetFruitAmount()
    {
        return _fruitAmount;
    }

    public int GetTotalFruitColected()
    {
        return _fruitColected + _fruitAmount;
    }

    private void CheckConditionToHeal()
    {
        if (_fruitAmount >= _fruitToHeal)
        {
            _fruitColected += _fruitAmount;
            _fruitAmount = 0;
            _player.Heal(1);
        }
    }

    public void RestartFruit()
    {
        _fruitAmount = 0;
        _fruitColected = 0;
    }
}
