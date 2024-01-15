using System.Collections;

namespace Model.Tasks
{
    public abstract class Task
    {
        public enum Priority
        {
            Min,
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

        public object Owner => _owner;

        public abstract IEnumerator Execute();
    }
}
