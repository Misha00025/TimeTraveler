using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameMap _gameMap;

    public static StateMachine Instance { get; private set; }
    
    private Queue<Destroyable> _destroyables = new Queue<Destroyable>();


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

    public void AddToDestroy(Destroyable destroyable, Model.Object owner)
    {
        TurnsManager.Instance.AddTask(new Model.Tasks.Destroy(destroyable, owner));
        //if (_destroyables.Contains(destroyable))
           // return;
        //_destroyables.Enqueue(destroyable);
    }

    public void Turn()
    {
        this._turnEnded.Invoke();
        EndTurn();
    }

    public void EndTurn()
    {
        StartCoroutine(TurnsManager.Instance.StartTurn());
        while (_destroyables.Count > 0)
        {
            Destroyable destroyable = _destroyables.Dequeue();
            destroyable.Destroy();
        }
    }
}
