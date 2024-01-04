using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class TimeTraveler
{
    // Возвращать игрока на _travelTurnsCount ходов назад
    [SerializeField] private int _travelTurnsCount = 3;
    [SerializeField] private int _usesLeft = 1;
    private readonly Queue<Vector2Int> _lastDirections = new();

    public void RememberDirection(Vector2Int cell) {
        if (_lastDirections.Count >= _travelTurnsCount)
        {
            _lastDirections.Dequeue();
        }
        _lastDirections.Enqueue(cell);
    }

    public void UseAbility(PlayerCharacter player)
    {
        if (_lastDirections.Count <= 0) 
        {
            Debug.LogError("Use ability without remembered directions");
            return;
        }
        if (_usesLeft-- <= 0)
        {
            Debug.LogError("No usees left :(");
            return;
        }

        var characterMover = player.Mover;
        foreach (var direction in _lastDirections)
        {
            if (_lastDirections.Count > 1)
            {
                characterMover.RawMove(direction * -1);
            } else
            {
                characterMover.Move(direction * -1);  
            }

        }
        _lastDirections.Clear();
    }
}
