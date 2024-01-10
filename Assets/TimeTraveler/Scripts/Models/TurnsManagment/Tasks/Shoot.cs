using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Shoot : Task
    {
        public Shoot(Movement.Direction direction, Object owner) : base(owner)
        {

        }

        public override IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
