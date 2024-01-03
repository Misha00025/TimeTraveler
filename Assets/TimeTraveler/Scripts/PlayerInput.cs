using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerCharacter _character;
    [SerializeField] private List<GameAction> gameActions;

    private MoveValidator _moveValidator;
    private CharacterMover Mover => this._character.Mover;

    public void Init(PlayerCharacter character, MoveValidator validator)
    {
        this._character = character;
        this._moveValidator = validator;
    }

    void Update()
    {
        Vector2Int direction = Vector2Int.zero;
        bool move = false;
        var action = this.GetAction();
        switch (action)
        {
            case GameAction.Left:
            case GameAction.Right:
            case GameAction.Up:
            case GameAction.Down:
                move = this._character.Mover.TryCastDirection(action, out direction);
                break;
            case GameAction.MainAction:
                Debug.Log("Main Action");
                break;
        }

        if (move)
        {
            Vector3Int position = this.Mover.CellPosition;
            Vector3Int newPosition = this.Mover.CalculateNewPosition(direction);
            Debug.Log($"Try move! Direction: {direction}; Next Position: {newPosition}");
            bool canMove = this._moveValidator.CanMove(position, newPosition);
            if (canMove)
            {
                this.Mover.Move(direction);
            }
            StateMachine.Instance.EndTurn();
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
