using Model.Tasks;

namespace Model
{
    public interface IMover
    {
        public bool TryMove(Object obj, Movement.Direction direction);
        public void Move(Object obj, Movement.Direction direction);
    }

    public class Mover : IMover
    {
        private IGameMap _gameMap;

        public Mover(IGameMap gameMap)
        {
            _gameMap = gameMap;
        }

        public virtual void Move(Object obj, Movement.Direction direction)
        {
            var position = _gameMap.GetPosition(obj);
            var nextPosition = _gameMap.GetCell(position, direction);
            _gameMap.Remove(obj);
            _gameMap.Set(obj, nextPosition);
        }

        public virtual bool TryMove(Object obj, Movement.Direction direction)
        {
            Move(obj, direction);
            return true;
        }
    }
}
