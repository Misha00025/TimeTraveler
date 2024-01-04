using System.Collections;
using UnityEngine;

public interface IMovable
{
    void Move(Vector2Int direction);
}

public class CharacterMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed = 1f;
    private GameMap _gameMap;
    private Vector3Int _cellPosition;
    private Coroutine _coroutine;

    public Vector2 Position2D => new Vector2(this.transform.position.x, this.transform.position.y);
    public Vector3Int CellPosition => _cellPosition;

    public void Init(GameMap gameMap)
    {
        if (this._gameMap == null)
        {
            this._gameMap = gameMap;
            this._cellPosition = gameMap.GetCell(Position2D);
        }
    }

    public Vector3Int CalculateNewPosition(Vector2Int direction)
    {
        Vector3Int position = this.CellPosition;
        Vector3Int newPosition = position + (Vector3Int)direction;
        return newPosition;
    }

    public void Move(Vector2Int direction)
    {
        Vector3Int newPosition = this.CalculateNewPosition(direction);
        this._cellPosition = newPosition;
        Vector3 target = this._gameMap.GetCellWorldPosition(newPosition);
        this._gameMap.Set(gameObject, newPosition);
        if (this._coroutine != null)
            StopCoroutine(this._coroutine);
        _coroutine = StartCoroutine(SmoothMovement(target));
    }

    public void RawMove(Vector2Int direction)
    {
        Vector3Int newPosition = this.CalculateNewPosition(direction);
        this._cellPosition = newPosition;
        Vector3 target = this._gameMap.GetCellWorldPosition(newPosition);

        if (this._coroutine != null)
            StopCoroutine(this._coroutine);
        _coroutine = StartCoroutine(SmoothMovement(target));
    }

    private IEnumerator SmoothMovement(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        while (Mathf.Abs((transform.position - target).magnitude) > 0.001)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, (_speed * Time.deltaTime));
            yield return null;
        }
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
