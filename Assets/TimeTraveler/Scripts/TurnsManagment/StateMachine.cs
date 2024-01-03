using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddTurnable(ITurnable turnable)
    {
        this._turnables.Add(turnable);
    }

    public void EndTurn()
    {
        foreach (var turnable in _turnables)
        {
            if (turnable != null)
            {
                turnable.OnTurn();
            }
        }
    }
}
