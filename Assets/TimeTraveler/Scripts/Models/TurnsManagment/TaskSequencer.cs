using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public abstract class Task
    {
        public enum Priority
        {
            Low,
            Medium,
            High,
            Max
        }

        private readonly object _owner;

        public Task(object owner)
        {
            _owner = owner;
        }

        public object Owner { get { return _owner; } }

        public abstract IEnumerator Execute();
    }


    public interface ITaskSequencer
    {
        int GetTasksCount(Task.Priority priority);
        void AddTask(Task task, Task.Priority priority = Task.Priority.Medium);
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
                yield return task.Execute();
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
