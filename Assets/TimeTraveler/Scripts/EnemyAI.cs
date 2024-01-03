using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITurnable
{
    [SerializeField] private List<GameAction> _moves = new List<GameAction>();
    [SerializeField] private bool _isLooped = false;

    private CharacterMover _characterMover;
    private Vector2Int direction;
    private int moveStep = 0; 
 
    // Start is called before the first frame update
    void Start()
    {
        _characterMover = GetComponent<CharacterMover>();
    }

    public void OnTurn()
    {

        if (moveStep < _moves.Count)
        {
            var currentMove = _moves[moveStep++];
        }

        if (moveStep >= _moves.Count && _isLooped)
        {
            moveStep = 0;
        }


        Debug.Log("My turn!");
    }
}
