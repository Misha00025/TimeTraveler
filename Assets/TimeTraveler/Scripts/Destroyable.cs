using System.Collections;
using UnityEngine;

public class Destroyable : MonoBehaviour
{

    public void Destroy(GameMap gameMap)
    {
        Debug.Log("������� ������");
        gameMap.Remove(gameObject);
        Destroy(gameObject);
    }
}
