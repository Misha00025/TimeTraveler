using Model;
using System.Threading;
using UnityEngine;

public class TestTurns : MonoBehaviour
{
    private readonly TaskSequencer _taskSequencer = new TaskSequencer();
    private bool _isRunning = false;

    private class Log : Task
    {
        private string _message;
        public Log(string message) : base(null)
        {
            _message = message;
        }

        public override void Execute()
        {
            Debug.Log(_message);
            Thread.Sleep(500);
        }
    }

    private class LogedTurn : Model.Turn
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
            return;
        var turn = new LogedTurn(_taskSequencer);
        turn.AddListenerToStart(() => { _isRunning = true; });
        turn.AddListenerToEnd(() => { _isRunning = false; });

        AddLog(Task.Priority.Low);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Low);
        AddLog(Task.Priority.Hight);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Hight);
        AddLog(Task.Priority.Medium);
        AddLog(Task.Priority.Max);

        turn.Run();
    }

    private void AddLog(Task.Priority priority)
    {
        _taskSequencer.AddTask(new TestTurns.Log($"Task priority: {priority}"), priority);
    }
}
