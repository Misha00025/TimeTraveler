using Model;
using Model.Tasks;
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
    private int moveStep = -1;

    private Unit _unit = new Unit();

    public void Start()
    {
        _characterMover = GetComponent<CharacterMover>();
        _bulletFactory = GetComponent<BulletFactory>();
    }

    public void Init(GameMap gameMap)
    {
        _gameMap = gameMap;
        _characterMover.Init(_gameMap);
        _moveValidator = new MoveValidator(_gameMap);
        if (TryGetComponent<Destroyable>(out var destroyable))
            destroyable.Init(_gameMap);
        StateMachine.Instance.AddTurnable(this);
    }

    public void OnTurn()
    {
        if (moveStep < _actions.Count)
        {
            moveStep++;
            if (moveStep >= _actions.Count && _isMovesLooped)
            {
                moveStep = 0;
            }
            var currentAction = _actions[moveStep];

            bool isMove = _characterMover.TryCastDirection(currentAction, out var direction);
            if (isMove)
            {
                var task = new CustomTask(Move, _unit);
                TurnsManager.Instance.AddTask(task);
            }

            if (currentAction == GameAction.Attack)
            {
                var task = new CustomTask(SpawnBullet, _unit);
                TurnsManager.Instance.AddTask(task);                
            }
        }
    }

    private IEnumerator SpawnBullet()
    {
        _bulletFactory.CreateBullet(_characterMover.CellPosition, currentDirection, _gameMap).OnTurn();
        yield return null;
    }

    private IEnumerator Move()
    {
        var currentAction = _actions[moveStep];
        _characterMover.TryCastDirection(currentAction, out var direction);
        Debug.Log($"Moving into{direction}");
        var newPosition = _characterMover.CalculateNewPosition(direction);
        bool canMove = _moveValidator.CanMove(_characterMover.CellPosition, newPosition);
        if (canMove)
        {
            _characterMover.Move(direction);
            currentDirection = direction;
            //while ((transform.position - _gameMap.GetCellWorldPosition(newPosition)).magnitude > 0.01)
            //{
            //}
        }

        yield return null;
    }
}
