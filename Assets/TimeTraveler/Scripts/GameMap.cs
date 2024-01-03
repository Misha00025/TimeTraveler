using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class GameMap : MonoBehaviour
{
    [SerializeField] private Tilemap _wallsTilemap;
    private List<Vector3Int> _wallsPositions = new List<Vector3Int>();
    private Grid _grid;

    public void Start()
    {
        this._grid = this.GetComponent<Grid>();
        this.FindAllWalls();
    }

    private void FindAllWalls()
    {
        // Поиск всех стен на указанном TileMap
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
}
