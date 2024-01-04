using System.Collections;
using UnityEngine;

public class Destroyable : MonoBehaviour
{

    public void Destroy(GameMap gameMap)
    {
        Debug.Log("׃האכÿול מבתוךע");
        gameMap.Remove(gameObject);
        Destroy(gameObject);
    }
}
