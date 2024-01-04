using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowFactory : MonoBehaviour
{
    [SerializeField] private Blow _blowPrefab;

    public Blow CreateBlow(Vector3Int cell, GameMap gameMap)
    {
        var currentPosition = gameMap.GetCellWorldPosition(cell);
        var blow = Instantiate(_blowPrefab, currentPosition, Quaternion.identity);

        StateMachine.Instance.AddTurnable(blow);

        return blow;
    }
}
