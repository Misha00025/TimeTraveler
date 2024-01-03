using UnityEditor.UI;
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
}
