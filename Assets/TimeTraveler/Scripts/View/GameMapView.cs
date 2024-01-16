using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GameMapView : View<Model.IGameMap>
{
    private Grid _grid;
    private List<View<Model.Object>> _objectsViews = new List<View<Model.Object>>();

    public Grid Grid => _grid;

    public void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    public override void SetModel(Model.IGameMap model) => base.SetModel(model);

    public void AddObjectView(View<Model.Object> view)
    {
        _objectsViews.Add(view);
    }

    public void RemoveObjectView(View<Model.Object> view)
    {
        _objectsViews.Remove(view);
    }

    public override void UpdateView()
    {
        foreach (var view in _objectsViews) 
        { 
            view.UpdateView();
        }
    }
}
