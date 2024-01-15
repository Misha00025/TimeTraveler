using System.Collections;

namespace Model
{
    public interface IDestroyable
    {
        void Destroy();
    }
}

namespace Model.Tasks
{
    public class Destroy : Task
    {
        IDestroyable _destroyable;

        public Destroy(IDestroyable destroyable, object owner) : base(owner)
        {
            _destroyable = destroyable;
        }

        public override IEnumerator Execute()
        {
            _destroyable.Destroy();
            yield return null;
        }
    }
}
