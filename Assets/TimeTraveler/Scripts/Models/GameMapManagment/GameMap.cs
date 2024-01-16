using Model.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Model
{
    public abstract class Object
    {
        public Vector3Int Cell { get; set; }
    }

    public class Wall : Object
    {

    }

    public class Unit : Object
    {

    }

    public class Bullet : Unit
    {

    }

    public class Player : Unit
    {

    }

    public interface IGameMap
    {
        public void Set(Object obj, UnityEngine.Vector3Int position);

        public void Remove(Object obj);

        public UnityEngine.Vector3Int GetPosition(Object obj);

        public UnityEngine.Vector3Int GetCell(Vector3Int position, Movement.Direction direction);

        public Object GetObject(UnityEngine.Vector3Int position);

        public bool IsOccuped(UnityEngine.Vector3Int position);
    }

    public class GameMap : IGameMap
    {
        private Dictionary<Object, UnityEngine.Vector3Int> _positions = new();
        private Dictionary<UnityEngine.Vector3Int, Object> _objects = new();

        public IReadOnlyDictionary<Object, UnityEngine.Vector3Int> Positions { get { return _positions; } }

        public void Set(Object obj, UnityEngine.Vector3Int position)
        {
            obj.Cell = position;
            _positions.Add(obj, position);
            _objects.Add(position, obj);
        }

        public void Remove(Object obj)
        {
            UnityEngine.Vector3Int position = _positions[obj];
            _objects.Remove(position);
            _positions.Remove(obj);
        }

        public UnityEngine.Vector3Int GetPosition(Object obj)
        {
            if (!_positions.ContainsKey(obj))
            {
                throw new Exception($"Dictionary in GameMap has not contain {obj}");
            }
            return _positions[obj];
        }

        public UnityEngine.Vector3Int GetCell(Vector3Int position, Movement.Direction direction)
        {
            Vector3Int offset = Vector3Int.zero;
            switch (direction)
            {
                case Movement.Direction.Up:
                    offset = Vector3Int.up;
                    break;
                case Movement.Direction.Down:
                    offset = Vector3Int.down;
                    break;
                case Movement.Direction.Left:
                    offset = Vector3Int.left;
                    break;
                case Movement.Direction.Right:
                    offset = Vector3Int.right;
                    break;
            }
            return position + offset;
        }

        public Object GetObject(UnityEngine.Vector3Int position)
        {
            if (this._objects.ContainsKey(position))
            {
                return null;
            }
            return this._objects[position];
        }

        public bool IsOccuped(UnityEngine.Vector3Int position)
        {
            return this._objects.ContainsKey(position);
        }
    }
}
