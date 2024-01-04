using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : Interactive
{
    [SerializeField] private Door _door;
    private SpriteRenderer _renderer;

    public void Start()
    {
        this._renderer = GetComponent<SpriteRenderer>();
    }

    public override void Use()
    {
        if (this._door.IsOpened)
        {
            _door.Close();
        }
        else
        {
            _door.Open();
        }
        _renderer.flipX = !_renderer.flipX;
    }
}
