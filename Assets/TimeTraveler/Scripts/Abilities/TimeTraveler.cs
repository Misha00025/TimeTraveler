using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class TimeTraveler
{
    // ���������� ������ �� _travelTurnsCount ����� �����
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

    public void UseAbility(PlayerView player)
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
        _lastDirections.Clear();
    }
}
