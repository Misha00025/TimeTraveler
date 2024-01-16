using System;
using System.Collections;

namespace Model.Tasks
{
    public class CustomTask : Task
    {
        private Func<IEnumerator> _action;

        public CustomTask(Func<IEnumerator> action, Object owner) : base(owner)
        {
            _action = action;
        }

        public override IEnumerator Execute()
        {
            yield return _action();
        }
    }
}
