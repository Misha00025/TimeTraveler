using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
     
    public Bullet CreateBullet(Vector3Int currentCell, Vector2Int direction, GameMap gameMap)
    {
        // spawn bullet on the next cell
        var currentPosition = gameMap.GetCellWorldPosition(currentCell + (Vector3Int)direction);
        var bullet = Instantiate(_bulletPrefab);

        bullet.transform.position = currentPosition;
        bullet.Init(direction, gameMap);
        return bullet;
    }
}
