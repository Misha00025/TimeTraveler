using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView
{
    void UpdateView();
}

public abstract class View<T> : MonoBehaviour, IView
{
    protected T _model;
    public T Model => _model;
    public virtual void SetModel(T model) { _model = model; }
    public abstract void UpdateView();
}
