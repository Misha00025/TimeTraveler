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

    void FixedUpdate()
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

        StartCoroutine( _currentTurn.Run() );        
    }

    private void AddLog(Task.Priority priority)
    {
        _taskSequencer.AddTask(new TestTurns.Log($"Task priority: {priority}"), priority);
    }
}
