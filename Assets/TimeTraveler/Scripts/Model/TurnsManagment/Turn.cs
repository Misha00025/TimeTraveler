using System;
using System.Linq;

namespace Model
{
    public class Turn
    {
        private readonly TaskSequencer _taskSequencer;
        private event Action _turnEnded;

        public Turn(TaskSequencer taskSequencer)
        {
            _taskSequencer = taskSequencer;
        }

        public void AddListener(Action turnEnded)
        {
            _turnEnded += turnEnded;
        }

        public async void Start()
        {
            var priorities = Enum.GetValues(typeof(Task.Priority)).Cast<Task.Priority>().ToArray();
            for (int i = priorities.Length - 1; i >= 0; i--) 
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    _taskSequencer.ExecuteTasks(priorities[i]);
                });
            }
            _turnEnded?.Invoke();
        }
    }
}
