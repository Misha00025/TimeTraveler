using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private GameMap _gameMap;

    [Header("Player")]
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private PlayerInput _input;

    // Start is called before the first frame update
    void Awake()
    {
        _gameMap.AddListenerToEndOfLoad(_player.Init);
        _gameMap.AddListenerToEndOfLoad(InitInput);
        this.InstanceEnemy();
    }

    private void InitInput(GameMap gameMap)
    {
        _input.Init(_player, new MoveValidator(gameMap));
    }

    private void InstanceEnemy()
    {
        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
        foreach (EnemyAI enemy in enemies)
        {
            _gameMap.AddListenerToEndOfLoad(enemy.Init);
        }
    }
}
