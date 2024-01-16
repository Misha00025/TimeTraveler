using System.Collections;


namespace Model.Tasks
{
    public class Movement : Task
    {
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        private Direction _direction;

        public Movement(Direction direction, Unit owner) : base(owner)
        {
            _direction = direction;
        }

        public override IEnumerator Execute()
        {
            Instancies.Mover.Move(Owner, _direction);
            yield return null;
        }
    }
}
