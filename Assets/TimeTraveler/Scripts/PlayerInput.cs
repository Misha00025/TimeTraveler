using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _character;
    [SerializeField] private GameMap _gameMap;
    [SerializeField] private List<GameAction> gameActions;

    public void Start()
    {
        _character.Mover.Init(_gameMap);
    }

    public void Init(PlayerCharacter character)
    {
        this._character = character;
    }

    void Update()
    {
        Vector2Int direction = Vector2Int.zero;
        var action = this.GetAction();
        switch (action)
        {
            case GameAction.Left:
            case GameAction.Right:
            case GameAction.Up:
            case GameAction.Down:
                direction = this._character.Mover.CastDirection(action);
                break;
            case GameAction.MainAction:
                Debug.Log("Main Action");
                break;
        }
        if (direction != Vector2Int.zero)
        {
            this._character.Mover.Move(direction);
            Debug.Log($"Direction: {direction}");
        }
    }

    private GameAction GetAction()
    {
        Dictionary<KeyCode, GameAction> GameActions = new Dictionary<KeyCode, GameAction>() 
        {
            { KeysContainer.Up, GameAction.Up },
            { KeysContainer.Down, GameAction.Down },
            { KeysContainer.Left, GameAction.Left },
            { KeysContainer.Right, GameAction.Right },
            { KeysContainer.MainAction, GameAction.MainAction }
        };
        foreach (KeyCode key in GameActions.Keys)
        {
            if (Input.GetKeyDown(key))
                return GameActions[key];
        }
        return GameAction.None;
    }
}
