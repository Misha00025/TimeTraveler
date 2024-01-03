using UnityEngine;

public class Bullet : MonoBehaviour, ITurnable
{
    [SerializeField, Range(1, 10)] private int speed = 2;
    private CharacterMover _characterMover;
    private Vector2Int _direction;
    private MoveValidator _validator;
    private GameMap _gameMap;

    private void Awake()
    {
        _characterMover = GetComponent<CharacterMover>();
    }

    public void Init(Vector2Int direction, GameMap gameMap) 
    {
        _validator = new MoveValidator(gameMap);
        _characterMover.Init(gameMap);
        _direction = direction;
        _gameMap = gameMap;
    }

    public void OnTurn()
    {
        for (int i=0; i<speed; i++) {
            var newPosition = _characterMover.CalculateNewPosition(_direction);
            if (_validator.CanMove(_characterMover.CellPosition, newPosition))
            {
                _characterMover.Move(_direction);
            }
            else
            {
                if (_gameMap.IsOccupied(newPosition))
                {
                    GameObject gameObject = _gameMap.GetGameObject(newPosition);
                    _gameMap.Remove(gameObject);
                    Destroy(gameObject);
                }
                _gameMap.Remove(gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
