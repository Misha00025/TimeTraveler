using System;
using System.Collections;

namespace Model
{
    public class CustomTask : Task
    {
        private Func<IEnumerator> _action;

        public CustomTask(Func<IEnumerator> action, object owner) : base(owner)
        {
            _action = action;
        }

        public override IEnumerator Execute()
        {
            yield return _action();
        }
    }
}
