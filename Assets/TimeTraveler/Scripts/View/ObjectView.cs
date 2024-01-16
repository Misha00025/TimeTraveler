using System.Collections;
using UnityEngine;

public class ObjectView : View<Model.Object>
{
    [SerializeField] private float _animationSpeed = 4.0f;

    private Vector3Int _cell;
    private Grid _grid;

    private Coroutine _moving;

    public void Init(Grid grid) 
    { 
        _grid = grid; 
    }

    public override void UpdateView()
    {
        if (_cell != _model.Cell)
        {
            _cell = _model.Cell;
            var worldPosition = _grid.GetCellCenterWorld(_cell);
            if (_moving != null)
            {
                StopCoroutine(_moving);
            }
            _moving = StartCoroutine(MoveTo(worldPosition));
        }
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while ((transform.position - target).magnitude > 0.01)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _animationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
