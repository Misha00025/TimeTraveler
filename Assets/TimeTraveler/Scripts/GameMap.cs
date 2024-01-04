using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class GameMap : MonoBehaviour
{
    [SerializeField] private Tilemap _wallsTilemap;
    private List<Vector3Int> _wallsPositions = new List<Vector3Int>();
    private Dictionary<Vector3Int, GameObject> _occupiedPositions = new();
    private Dictionary<GameObject, Vector3Int> _gameObjects = new();
    private Grid _grid;

    public void Start()
    {
        this._grid = this.GetComponent<Grid>();
        this.FindAllWalls();
    }

    private void FindAllWalls()
    {
        foreach (Vector3Int cellPosition in this._wallsTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = this._wallsTilemap.GetTile(cellPosition);
            if (tile != null)
            {
                this._wallsPositions.Add(cellPosition);
            }
        }
    }

    public Vector3Int GetCell(Vector2 position)
    {
        Vector3Int cell = this._grid.WorldToCell(position);
        return cell;
    }

    public Vector3 GetCellWorldPosition(Vector3Int cellGridPosition)
    {
        Vector3 position = this._grid.GetCellCenterWorld(cellGridPosition);
        return position;
    }

    public bool IsWall(Vector3Int position) => this._wallsPositions.Contains(position);

    public bool IsOccupied(Vector3Int position) => _occupiedPositions.ContainsKey(position);

    public void Set(GameObject gameObject, Vector3Int on)
    {
        if (_gameObjects.ContainsKey(gameObject))
        {
            Remove(gameObject);
        }
        _occupiedPositions.Add(on, gameObject);
        _gameObjects.Add(gameObject, on);
    }

    public void Remove(GameObject gameObject)
    {
        Vector3Int lastPosition = _gameObjects[gameObject];
        _gameObjects.Remove(gameObject);
        _occupiedPositions.Remove(lastPosition);
    }

    public GameObject GetGameObject(Vector3Int position)
    {
        return _occupiedPositions[position];
    }

    public bool TryGet<T>(Vector3Int position, out T result) where T : class
    {
        bool contain = this.IsOccupied(position);
        bool haveComponent = false;
        result = null;

        if (contain)
        {
            haveComponent = _occupiedPositions[position].TryGetComponent<T>(out result);
        }

        return contain && haveComponent;
    } 

}
