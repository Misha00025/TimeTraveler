using UnityEngine;

public interface IMoveValidator
{
    bool CanMoveTo(GameMap gameMap, Vector3Int position, Vector3Int newPosition);
}

public class MoveValidator : IMoveValidator
{
    public bool CanMoveTo(GameMap gameMap, Vector3Int position, Vector3Int newPosition)
    {
        float magnitude = (position - newPosition).magnitude;
        return gameMap.IsWall(newPosition) && magnitude < 1.1f;
    }
}