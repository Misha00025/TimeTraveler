using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
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

        public Movement(Direction direction, Object owner) : base(owner)
        {
            _direction = direction;
        }

        public override IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
