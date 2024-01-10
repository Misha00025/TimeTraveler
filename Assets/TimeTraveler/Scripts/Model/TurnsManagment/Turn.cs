using System;
using System.Linq;

namespace Model
{
    public class Turn
    {
        private const int _maxIterations = 1000;
        private readonly ITaskSequencer _taskSequencer;
        private event Action _turnStarted;
        private event Action _turnEnded;

        public Turn(ITaskSequencer taskSequencer)
        {
            _taskSequencer = taskSequencer;
        }

        public void AddListenerToStart(Action turnStarted)
        {
            _turnStarted += turnStarted;
        }

        public void AddListenerToEnd(Action turnEnded)
        {
            _turnEnded += turnEnded;
        }

        public async void Run()
        {
            Start(); 
            _turnStarted?.Invoke();
            int iteration = 0;
            while (!_taskSequencer.IsEmpty() && ++iteration <= _maxIterations)
            {
                await System.Threading.Tasks.Task.Run(Tick);
            }
            End();
            _turnEnded?.Invoke();
        }

        protected virtual void Start() { }
        protected virtual void End() { }

        private void Tick()
        {
            var priorities = Enum.GetValues(typeof(Task.Priority)).Cast<Task.Priority>().ToArray();
            for (int i = priorities.Length - 1; i >= 0; i--)
            {                
                _taskSequencer.ExecuteTasks(priorities[i]);
            }
        }
    }
}
