using Model;
using System.Collections;
using UnityEngine;

public class Destroyable : MonoBehaviour, Model.IDestroyable
{
    private GameMap _gameMap;

    public void Init(GameMap gameMap)
    {
        _gameMap = gameMap;
    }

    public void Destroy()
    {
        Debug.Log("׃האכÿול מבתוךע");
        //_gameMap.Remove();
        Destroy(gameObject);
    }
}
