using Codice.CM.Client.Differences.Merge;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Tasks
{
    public class Shoot : Task
    {
        private Movement.Direction _direction;

        public Shoot(Movement.Direction direction, Object owner) : base(owner)
        {
            _direction = direction;
        }

        public override IEnumerator Execute()
        {
            Debug.Log($"Task: Shoot; Direction: {_direction}; Owner: {Owner}");
            yield return null;
        }
    }
}
