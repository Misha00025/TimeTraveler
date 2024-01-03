using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITurnable
{
    [SerializeField] private GameMap _gameMap;
    [SerializeField] private List<GameAction> _actions = new List<GameAction>();
    [SerializeField] private bool _isMovesLooped = true;

    private CharacterMover _characterMover;
    private Vector2Int currentDirection;
    private int moveStep = 0;

    // Start is called before the first fr ame update
    void Start()
    {
        _characterMover = GetComponent<CharacterMover>();
        _characterMover.Init(_gameMap);
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
                _characterMover.Move(direction);
                currentDirection = direction;
            }
        }

        if (moveStep >= _actions.Count && _isMovesLooped)
        {
            moveStep = 0;
        }

        Debug.Log("My turn!");
    }

    float counter = 0;
    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= 1)
        {
            OnTurn();
            counter = 0;
        }
    }
}
