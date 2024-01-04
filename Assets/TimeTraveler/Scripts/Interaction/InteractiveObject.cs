using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Interactive _interactive;

    public void Use()
    {
        _interactive.Use();
    }
}
