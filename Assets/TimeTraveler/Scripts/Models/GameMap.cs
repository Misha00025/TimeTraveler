using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class Object
    {

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

    public class GameMap
    {
        private Dictionary<Object, UnityEngine.Vector2Int> _positions = new();
        private Dictionary<UnityEngine.Vector2Int, Object> _objects = new();

        public void Set(Object obj, UnityEngine.Vector2Int position)
        {
            _positions.Add(obj, position);
            _objects.Add(position, obj);
        }

        public void Remove(Object obj)
        {
            UnityEngine.Vector2Int position = _positions[obj];
            _objects.Remove(position);
            _positions.Remove(obj);
        }

        public UnityEngine.Vector2Int GetPosition(Object obj)
        {
            if (_positions.ContainsKey(obj))
            {
                throw new Exception($"Dictionary in GameMap has not contain {obj}");
            }
            return _positions[obj];
        }

        public Object GetObject(UnityEngine.Vector2Int position)
        {
            if (this._objects.ContainsKey(position))
            {
                return null;
            }
            return this._objects[position];
        }

        public bool IsOccuped(UnityEngine.Vector2Int position)
        {
            return this._objects.ContainsKey(position);
        }
    }
}
