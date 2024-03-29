﻿using UnityEngine;

public interface IMoveValidator
{
    bool CanMove(Vector3Int position, Vector3Int newPosition);
}

public class MoveValidator : IMoveValidator
{
    private GameMap _gameMap;

    public MoveValidator(GameMap gameMap) 
    { 
        this._gameMap = gameMap;
    }

    public bool CanMove(Vector3Int from, Vector3Int to)
    {
        float magnitude = (from - to).magnitude;
        return !this._gameMap.IsWall(to) && magnitude < 1.1f && !this._gameMap.IsOccupied(to);
    }
}