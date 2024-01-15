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

        public Movement(Direction direction, GameMap gameMap, Unit owner) : base(owner)
        {
            _direction = direction;
        }

        public override IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
