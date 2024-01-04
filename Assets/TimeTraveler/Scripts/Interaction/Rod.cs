using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : Interactive
{
    [SerializeField] private Door _door;

    public override void Use()
    {
        _door.Open();
    }
}
