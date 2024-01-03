using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITurnable
{
    [SerializeField] private GameMap _gameMap;
    [SerializeField] private List<GameAction> _actions = new List<GameAction>();
    [SerializeField] private bool _isMovesLooped = true;
    [SerializeField] private Vector2Int currentDirection = Vector2Int.right;

    private MoveValidator _moveValidator;
    private CharacterMover _characterMover;
    private BulletFactory _bulletFactory;
    private int moveStep = 0;

    public void Start()
    {
        _characterMover = GetComponent<CharacterMover>();
        _bulletFactory = GetComponent<BulletFactory>();
        _characterMover.Init(_gameMap);
        _moveValidator = new MoveValidator(_gameMap);
        StateMachine.Instance.AddTurnable(this);
    }

    public void Init(GameMap gameMap)
    {
        _gameMap = gameMap;
    }

    public void OnTurn()
    {
        if (moveStep < _actions.Count)
        {
            var currentAction = _actions[moveStep++];

            bool isMove = _characterMover.TryCastDirection(currentAction, out var direction);
            if (isMove)
            {
                Debug.Log($"Moving into{direction}");
                bool canMove = _moveValidator.CanMove(_characterMover.CellPosition, _characterMover.CalculateNewPosition(direction));
                if (canMove)
                {
                    _characterMover.Move(direction);
                    currentDirection = direction;
                }
            }

            if (currentAction == GameAction.Attack)
            {
                _bulletFactory.CreateBullet(_characterMover.CellPosition, currentDirection, _gameMap).OnTurn();
            }
        }

        if (moveStep >= _actions.Count && _isMovesLooped)
        {
            moveStep = 0;
        }

        Debug.Log("My turn!");
    }
}
