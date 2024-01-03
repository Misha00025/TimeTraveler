using UnityEngine;

public class Bullet : MonoBehaviour, ITurnable
{
    [SerializeField, Range(1, 10)] private int speed = 2;
    private CharacterMover _characterMover;
    private Vector2Int _direction;
    private MoveValidator _validator;

    private void Awake()
    {
        _characterMover = GetComponent<CharacterMover>();
    }

    public void Init(Vector2Int direction, GameMap gameMap) 
    {
        _validator = new MoveValidator(gameMap);
        _characterMover.Init(gameMap);
        _direction = direction;
    }

    public void OnTurn()
    {
        for (int i=0; i<speed; i++) {
            if (_validator.CanMove(_characterMover.CellPosition, _characterMover.CalculateNewPosition(_direction)))
                _characterMover.Move(_direction);
            else
                Destroy(gameObject);
        }
    }
}