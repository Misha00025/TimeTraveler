using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class Turn
    {
        private const int _maxIterations = 1000;
        private readonly ITaskSequencer _taskSequencer;

        private event Action<bool> _pause;
        private bool _paused = false;

        private event Action _turnStarted;
        private event Action _turnEnded;

        public Turn(ITaskSequencer taskSequencer)
        {
            _taskSequencer = taskSequencer;
        }

        public bool Paused => _paused;

        public void AddListenerToStart(Action turnStarted)
        {
            _turnStarted += turnStarted;
        }

        public void AddListenerToEnd(Action turnEnded)
        {
            _turnEnded += turnEnded;
        }

        public IEnumerator Run()
        {
            Start(); 
            _turnStarted?.Invoke();
            int iteration = 0;
            while (!_taskSequencer.IsEmpty() && ++iteration <= _maxIterations)
            {
                yield return Tick();
            }
            End();
            _turnEnded?.Invoke();
        }

        public void Continue()
        {
            _paused = false;
            _pause?.Invoke(false);
        }

        public void Pause()
        {
            _paused = true;
            _pause?.Invoke(true);
        }

        protected virtual void Start() { }
        protected virtual void End() { }

        private IEnumerator Tick()
        {
            var priorities = Enum.GetValues(typeof(Task.Priority)).Cast<Task.Priority>().ToArray();
            for (int i = priorities.Length - 1; i >= 0; i--)
            {
                yield return _taskSequencer.ExecuteTasks(priorities[i]);
            }
        }
    }
}
