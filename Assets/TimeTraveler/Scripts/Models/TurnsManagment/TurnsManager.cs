using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    internal static class PriorityEvaluator
    {
        private const Task.Priority _defaultPriority = Task.Priority.Min;

        public static Task.Priority GetPriority(Task task)
        {
            
            switch (task)
            {
                case Movement:
                    return GetMovementPriority(task);
                case Shoot:
                    return Task.Priority.Low;
                default:
                    return _defaultPriority;
            }
        }

        private static Task.Priority GetMovementPriority(Task task)
        {
            switch (task.Owner)
            {
                case Player:
                    return Task.Priority.Max;
                case Unit: 
                    return Task.Priority.High;
                case Bullet:
                    return Task.Priority.Medium;
                default:
                    return _defaultPriority;
            }
        }
    }

    public class TurnsManager
    {
        private static TurnsManager _instance;
        public static TurnsManager Instance { 
            get 
            {
                if (_instance == null)
                    _instance = new TurnsManager();
                return _instance;
            }
        }

        private TaskSequencer _taskSequencer;
        private Dictionary<object, int> _owners;

        private class DefferedTask : Task
        {
            private Task _task;
            private int _turnsCount;
            private TurnsManager _turnsManager;

            public DefferedTask(Task task, int turnsCount, TurnsManager turnsManager) : base(null)
            {
                _task = task;
                _turnsCount = turnsCount;
                _turnsManager = turnsManager;
            }

            public override IEnumerator Execute()
            {
                yield return null;
                var task = _task;
                if (--_turnsCount > 0)
                {
                    task = this;
                }
                _turnsManager.AddTask(task);
            }
        }

        private TurnsManager()
        {
            _taskSequencer = new TaskSequencer();
            _owners = new Dictionary<object, int>();
        }

        public void AddTask(Task task)
        {
            object owner = task.Owner;
            if (owner != null)
            {
                if (_owners.ContainsKey(task.Owner))
                {
                    task = new DefferedTask(task, _owners[owner], this);
                }
                else
                {
                    _owners.Add(owner, 0);
                }
                _owners[owner] = _owners[owner] + 1;
            }
            _taskSequencer.AddTask(task, PriorityEvaluator.GetPriority(task));
        }

        public IEnumerator StartTurn()
        {
            while (!_taskSequencer.IsEmpty())
            {
                var turn = new Turn(_taskSequencer);
                _owners.Clear();
                yield return turn.Run();
            }
        }
    }
}
