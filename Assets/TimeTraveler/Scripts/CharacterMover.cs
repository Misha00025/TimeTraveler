using UnityEngine;

public interface IMovable
{
    void Move(Vector2Int direction);
}

public class CharacterMover : MonoBehaviour, IMovable
{
    private GameMap _gameMap;

    public Vector2 Position2D => new Vector2(this.transform.position.x, this.transform.position.y);
    public Vector3Int CellPosition => this._gameMap.GetCell(this.Position2D);

    public void Init(GameMap gameMap)
    {
        if (this._gameMap == null)
        {
            this._gameMap = gameMap;
        }
    }

    public void Move(Vector2Int direction)
    {
        Vector3Int position = this.CellPosition;
        Vector3Int newPosition = position + (Vector3Int)direction;
        this.transform.position = this._gameMap.GetCellWorldPosition(newPosition);
    }

    public Vector2Int CastDirection(GameAction action)
    {
        var direction = Vector2Int.zero;
        switch(action)
        {
            case GameAction.Left:
                direction = Vector2Int.left;
                break;
            case GameAction.Right:
                direction = Vector2Int.right;
                break;
            case GameAction.Up:
                direction = Vector2Int.up;
                break;
            case GameAction.Down:
                direction = Vector2Int.down;
                break;
        }
        return direction;
    }

    public bool TryCastDirection(GameAction action, out Vector2Int direction)
    {
        direction = CastDirection(action);
        return direction != Vector2Int.zero;
    }
}
