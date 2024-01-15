using System;
using System.Collections;
using System.Collections.Generic;
using Model.Tasks;

namespace Model
{
    public interface IAddetiveTaskSequencer
    {
        void AddTask(Task task, Task.Priority priority = Task.Priority.Medium);
    }


    public interface ITaskSequencer : IAddetiveTaskSequencer
    {
        int GetTasksCount(Task.Priority priority);        
        IEnumerator ExecuteTasks(Task.Priority priority);
        bool IsEmpty();
    }


    public class TaskSequencer : ITaskSequencer
    {
        private readonly Dictionary<Task.Priority, List<Task>> _tasks;

        public TaskSequencer()
        {
            _tasks = new Dictionary<Task.Priority, List<Task>>();
            foreach (Task.Priority priority in Enum.GetValues(typeof(Task.Priority)))
            {
                _tasks.Add(priority, new List<Task>());
            }
        }

        public int GetTasksCount()
        {
            int result = 0;
            foreach (Task.Priority priority in Enum.GetValues(typeof(Task.Priority)))
            {
                result += _tasks[priority].Count;
            }
            return result;
        }

        public int GetTasksCount(Task.Priority priority)
        {
            return _tasks[priority].Count;
        }

        public void AddTask(Task task, Task.Priority priority = Task.Priority.Medium)
        {
            _tasks[priority].Add(task);
        }

        public IEnumerator ExecuteTasks(Task.Priority priority)
        {
            Task[] tasks = _tasks[priority].ToArray();
            _tasks[priority].Clear();
            foreach (Task task in tasks)
            {
                try
                {
                    yield return task.Execute();
                }
                finally
                {

                }
            }
        }

        public bool IsEmpty()
        {
            int count = 0;
            foreach (Task.Priority priority in Enum.GetValues(typeof(Task.Priority)))
            {
                count += GetTasksCount(priority);
            }
            return count == 0;
        }
    }
}