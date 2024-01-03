using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _character;
    [SerializeField] private GameMap _gameMap;

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
        switch (this.GetAction())
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
