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

        private readonly Object _owner;

        public Task(Object owner)
        {
            _owner = owner;
        }

        public Object Owner => _owner;

        public abstract IEnumerator Execute();
    }
}
