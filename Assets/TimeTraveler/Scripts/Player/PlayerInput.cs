using Model;
using Model.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private List<GameAction> gameActions;
    [SerializeField] private TimeTraveler _timeTraveler;
    private Player _player;

    public void Init(Player player)
    {
        this._player = player;
    }

    void Update()
    {
        bool move = false;
        var action = this.GetAction();
        var direction = Movement.Direction.Down;
        switch (action)
        {
            case GameAction.Left:
            case GameAction.Right:
            case GameAction.Up:
            case GameAction.Down:
                direction = CastDirection(action);
                move = true;
                break;
            case GameAction.TimeTravelAbility:
                Debug.Log("Travel Action");
                //_timeTraveler.UseAbility(this._character);
                break;
            case GameAction.MainAction:
                Debug.Log("Main Action");
                break;
        }

        if (move)
        {
            var task = new Movement(direction, _player);
            TurnsManager.Instance.AddTask(task);
            StateMachine.Instance.Turn();
        }
    }

    private Movement.Direction CastDirection(GameAction action)
    {
        switch (action)
        {
            case GameAction.Up:
                return Movement.Direction.Up;
            case GameAction.Down:
                return Movement.Direction.Down;
            case GameAction.Left:
                return Movement.Direction.Left;
            case GameAction.Right:
                return Movement.Direction.Right;
        }
        return Movement.Direction.Up;
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
