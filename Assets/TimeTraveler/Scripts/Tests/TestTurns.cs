using Model;
using System.Collections;
using UnityEngine;

public class TestTurns : MonoBehaviour
{
    private readonly TaskSequencer _taskSequencer = new TaskSequencer();
    private bool _isRunning = false;
    private Turn _currentTurn;

    private class Log : Task
    {
        private string _message;
        public Log(string message) : base(null)
        {
            _message = message;
        }

        public override IEnumerator Execute()
        {
            Debug.Log(_message);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private class LogedTurn : Turn
    {
        public LogedTurn(TaskSequencer taskSequencer) : base(taskSequencer) { }

        protected override void Start()
        {
            base.Start();
            Debug.Log("Turn started!");
        }

        protected override void End()
        {
            base.End();
            Debug.Log("Turn ended!");
        }
    }

    public void Start()
    {
        Test2();
    }

    private void Test1()
    {
        if (_isRunning)
        {
            return;
        }
        var turn = new LogedTurn(_taskSequencer);
        _currentTurn = turn;
        turn.AddListenerToStart(() => { _isRunning = true; });
        turn.AddListenerToEnd(() => { _isRunning = false; });

        AddLog(Task.Priority.Low);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Low);
        AddLog(Task.Priority.High);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.High);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Max);

        StartCoroutine(_currentTurn.Run());
    }

    private void Test2()
    {
        if (_isRunning)
        {
            return;
        }
        Player player = new Player();
        Unit unit = new Unit();
        Model.Bullet bullet = new Model.Bullet();

        AddTask(new Movement(Movement.Direction.Left, player));
        AddTask(new Movement(Movement.Direction.Right, player));
        AddTask(new Movement(Movement.Direction.Up, player));
        AddTask(new Movement(Movement.Direction.Up, player));
        AddTask(new Movement(Movement.Direction.Up, player));
        AddTask(new Movement(Movement.Direction.Up, player));

        /*AddTask(new Movement(Movement.Direction.Left, bullet));
        AddTask(new Movement(Movement.Direction.Right, bullet));
        AddTask(new Movement(Movement.Direction.Left, bullet));
        AddTask(new Movement(Movement.Direction.Left, bullet));

        AddTask(new Movement(Movement.Direction.Left, unit));
        AddTask(new Movement(Movement.Direction.Right, unit));
        AddTask(new Movement(Movement.Direction.Up, unit));
        AddTask(new Movement(Movement.Direction.Up, unit));*/
        

        StartCoroutine( TurnsManager.Instance.StartTurn() );
    }

    private void AddLog(Task.Priority priority)
    {
        _taskSequencer.AddTask(new Log($"Task priority: {priority}"), priority);
    }

    private void AddTask(Task task)
    {
        TurnsManager.Instance.AddTask(task);
    }
}
