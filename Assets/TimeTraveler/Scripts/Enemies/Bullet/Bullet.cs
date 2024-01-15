using Model;
using Model.Tasks;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, ITurnable
{
    [SerializeField, Range(1, 10)] private int speed = 2;
    private CharacterMover _characterMover;
    private Vector2Int _direction;
    private MoveValidator _validator;
    private GameMap _gameMap;
    private BlowFactory _blowFactory;

    private Model.Bullet _bullet;

    private void Awake()
    {
        _characterMover = GetComponent<CharacterMover>();
        _blowFactory = GetComponent<BlowFactory>();
    }

    public void Init(Vector2Int direction, GameMap gameMap) 
    {
        _validator = new MoveValidator(gameMap);
        _characterMover.Init(gameMap);
        _direction = direction;
        _gameMap = gameMap;
        _bullet = new Model.Bullet();
        GetComponent<Destroyable>().Init(_gameMap);
    }

    public void OnTurn()
    {
        for (int i=0; i < speed; i++) {
            var task = new CustomTask(Action, _bullet);          
            TurnsManager.Instance.AddTask(task);
        }
    }

    private IEnumerator Action()
    {
        if (_characterMover == null)
        {
            yield return null;
        }
        var newPosition = _characterMover.CalculateNewPosition(_direction);
        if (_validator.CanMove(_characterMover.CellPosition, newPosition))
        {
            _characterMover.Move(_direction);
        }
        else
        {
            var task = new CustomTask(() =>
            {
                _characterMover.RawMove(_direction);
                var blow = _blowFactory.CreateBlow(newPosition, _gameMap);
                StateMachine.Instance.AddTurnable(blow);

                if (_gameMap.TryGet<Destroyable>(newPosition, out var destroyable))
                {
                    StateMachine.Instance.AddToDestroy(destroyable, _bullet);
                }
                StateMachine.Instance.AddToDestroy(GetComponent<Destroyable>(), _bullet);
                return null;
            }, _bullet);   
            TurnsManager.Instance.AddTask(task);
        }
        yield return null;
    }


}
