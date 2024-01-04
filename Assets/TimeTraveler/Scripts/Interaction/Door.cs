using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactive
{
    [SerializeField] private bool _isOpened = false;
    [SerializeField] private Sprite _spriteClose;
    [SerializeField] private Sprite _spriteOpen;
    private SpriteRenderer _renderer;

    public bool IsOpened => _isOpened;

    public void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Close();
    }

    public override void Use()
    {
        if ( _isOpened )
        {
            Debug.Log("You Win!");
            GameManager.Instance.Win();
        }
        else
        {
            Debug.Log("Please, open door");
        }
    }

    public void Open()
    {
        _isOpened = true;
        _renderer.sprite = _spriteOpen;
    }

    public void Close()
    {
        _isOpened = false;
        _renderer.sprite = _spriteClose;
    }
}
