using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private List<ITurnable> _turnables = new List<ITurnable>();
    private UnityEvent _turnEnded = new UnityEvent();

    public void AddTurnable(ITurnable turnable)
    {
        this._turnables.Add(turnable);
        this._turnEnded.AddListener(turnable.OnTurn);
    }

    public void EndTurn()
    {
        this._turnEnded.Invoke();
    }
}
