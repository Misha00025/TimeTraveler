using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blow : MonoBehaviour, ITurnable
{
    [SerializeField] private int _blowDuration = 1;
    private int _blowTimeLeft = 0;

    void Start()
    {
        _blowTimeLeft = _blowDuration;
    }

    public void OnTurn()
    {
        if (_blowTimeLeft-- <= 0) {
            Destroy(gameObject);
        }
    }
}
