using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class PlayerCharacter : MonoBehaviour
{
    private CharacterMover _characterMover;

    public CharacterMover Mover => this._characterMover;

    public void Start()
    {
        this._characterMover = this.GetComponent<CharacterMover>();
    }
}
