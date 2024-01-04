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
        _gameMap.AddListenerToEndOfLoad(InitInteractive);
        this.InstanceEnemy();
    }

    private void InitInput(GameMap gameMap)
    {
        _input.Init(_player, new MoveValidator(gameMap), gameMap);
    }

    private void InitInteractive(GameMap gameMap)
    {
        InteractiveObject[] interactives = FindObjectsByType<InteractiveObject>(FindObjectsSortMode.None);
        foreach (InteractiveObject interactiveObject in interactives)
        {
            Vector3Int cell = _gameMap.GetCell(interactiveObject.transform.position);
            interactiveObject.transform.position = _gameMap.GetCellWorldPosition(cell);
            _gameMap.Set(interactiveObject.gameObject, cell);
        }
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
