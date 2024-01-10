using System.Collections;

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
}
