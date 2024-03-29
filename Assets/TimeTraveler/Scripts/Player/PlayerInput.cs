using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerCharacter _character;
    [SerializeField] private List<GameAction> gameActions;
    [SerializeField] private TimeTraveler _timeTraveler;
    private MoveValidator _moveValidator;
    private GameMap _gameMap;

    private CharacterMover Mover => this._character.Mover;

    public void Init(PlayerCharacter character, MoveValidator validator, GameMap gameMap)
    {
        this._character = character;
        this._moveValidator = validator;
        this._gameMap = gameMap;
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
            case GameAction.TimeTravelAbility:
                Debug.Log("Travel Action");
                _timeTraveler.UseAbility(this._character);
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
                _timeTraveler.RememberDirection(direction);
            }
            else
            {
                if (_gameMap.TryGet<InteractiveObject>(newPosition, out var interactive))
                {
                    interactive.Use();
                }
            }
            StateMachine.Instance.Turn();
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
            { KeysContainer.MainAction, GameAction.MainAction },
            { KeysContainer.TimeTravelAbility, GameAction.TimeTravelAbility },
        };
        foreach (KeyCode key in GameActions.Keys)
        {
            if (Input.GetKeyDown(key))
                return GameActions[key];
        }
        return GameAction.None;
    }
}
