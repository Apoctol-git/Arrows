using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventAggregator
{
    public static OnAddingBlockEvent OnAddingBlockEvent = new OnAddingBlockEvent();
    public static OnUpdatingBlockEvent OnUpdatingBlockEvent = new OnUpdatingBlockEvent();
    public static OnUpdatedBlockEvent OnUpdatedBlockEvent = new OnUpdatedBlockEvent();
    public static OnSwapBlockEvent OnSwapBlockEvent = new OnSwapBlockEvent();
    public static OnDeletingBlockEvent OnDeletingBlockEvent = new OnDeletingBlockEvent();
    public static SelectBlockRequestEvent SelectBlockRequestEvent = new SelectBlockRequestEvent();
    public static OnClickBlockEvent OnClickBlockEvent = new OnClickBlockEvent();
    public static OnDataBaseCreated OnDataBaseCreated = new OnDataBaseCreated();
    public static GetBlocksRequestEvent getBlocksRequestEvent = new GetBlocksRequestEvent();
    public static OnEndLockClickButton OnEndLockClickButton = new OnEndLockClickButton();
    public static GetIsLockState GetIsLockState = new GetIsLockState();
    public static OnNewGameClick OnNewGameClick = new OnNewGameClick();
    public static OnEndGameChecking OnEndGameChecking = new OnEndGameChecking();
    public static OnCleaningDataBase OnCleaningDataBase = new OnCleaningDataBase();
}
public class OnAddingBlockEvent
{
    private List<Action<GameObject>> callbacks = new List<Action<GameObject>>();
    public void Subscribe(Action<GameObject> callback)
    {
        callbacks.Add(callback);
    }
    public void Publish(GameObject block)
    {
        foreach (var item in callbacks)
        {
            item(block);
        }
    }
}
public class OnUpdatingBlockEvent
{
    private List<Action<Vector3, GameObject>> callbacks = new List<Action<Vector3, GameObject>>();
    public void Subscribe(Action<Vector3, GameObject> callback)
    {
        callbacks.Add(callback);
    }
    public void Publish(Vector3 oldCoordinates, GameObject block)
    {
        foreach (var item in callbacks)
        {
            item(oldCoordinates,block);
        }
    }
}
public class OnUpdatedBlockEvent
{
    private List<Action> callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        callbacks.Add(callback);
    }
    public void Publish()
    {
        foreach (var item in callbacks)
        {
            item();
        }
    }
}
public class OnSwapBlockEvent
{
    private List<Action<Vector3, GameObject, Vector3, GameObject>> callbacks = new List<Action<Vector3, GameObject, Vector3, GameObject>>();
    public void Subscribe(Action<Vector3, GameObject, Vector3, GameObject> callback)
    {
        callbacks.Add(callback);
    }
    public void Publish(Vector3 oldCoordinatesClickedBlock, GameObject clickedBlock, Vector3 oldCoordinatesNeighborBlock, GameObject neighborBlock)
    {
        foreach (var item in callbacks)
        {
            item(oldCoordinatesClickedBlock, clickedBlock, oldCoordinatesNeighborBlock, neighborBlock);
        }
    }
}
public class OnDeletingBlockEvent
{
    private List<Action<Vector3>> callbacks = new List<Action<Vector3>>();
    public void Subscribe(Action<Vector3> callback)
    {
        callbacks.Add(callback);
    }
    public void Publish(Vector3 coordinates)
    {
        foreach (var item in callbacks)
        {
            item(coordinates);
        }
    }
}
public class SelectBlockRequestEvent
{
    private List<Func<Vector3, GameObject>> callbacks = new List<Func<Vector3, GameObject>>();
    public void Subscribe(Func<Vector3, GameObject> callback)
    {
        callbacks.Add(callback);
    }
    public GameObject Publish(Vector3 coordinates)
    {     
        return callbacks[0](coordinates) ;
    }
}
public class OnClickBlockEvent
{
    private List<Action<GameObject>> callbacks = new List<Action<GameObject>>();
    public void Subscribe(Action<GameObject> callback)
    {
        callbacks.Add(callback);
    }
    public void Publish(GameObject block)
    {
        foreach (var item in callbacks)
        {
            item(block);
        }
    }
}
public class OnDataBaseCreated
{
    private List<Action> callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        callbacks.Add(callback);
    }
    public void Publish()
    {
        foreach (var item in callbacks)
        {
            item();
        }
    }
}
public class GetBlocksRequestEvent
{
    private List<Func<Dictionary<Vector3, GameObject>>> callbacks = new List<Func<Dictionary<Vector3, GameObject>>>();
    public void Subscribe(Func<Dictionary<Vector3, GameObject>> callback)
    {
        callbacks.Add(callback);
    }
    public Dictionary<Vector3, GameObject> Publish()
    {
        return callbacks[0]();
    }
}
public class OnEndLockClickButton
{
    private List<Action<bool>> callbacks = new List<Action<bool>>();
    public void Subscribe(Action<bool> callback)
    {
        callbacks.Add(callback);
    }
    public void Publish(bool lockState)
    {
        foreach (var item in callbacks)
        {
            item(lockState);
        }
    }
}
public class GetIsLockState
{
    private List<Func<bool>> callbacks = new List<Func<bool>>();
    public void Subscribe(Func<bool> callback)
    {
        callbacks.Add(callback);
    }
    public bool Publish()
    {     
        return callbacks[0]() ;
    }
}
public class OnNewGameClick
{
    private List<Action> callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        callbacks.Add(callback);
    }
    public void Publish()
    {
        foreach (var item in callbacks)
        {
            item();
        }
    }
}
public class OnEndGameChecking
{
    private List<Action> callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        callbacks.Add(callback);
    }
    public void Publish()
    {
        foreach (var item in callbacks)
        {
            item();
        }
    }
}
public class OnCleaningDataBase
{
    private List<Action> callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        callbacks.Add(callback);
    }
    public void Publish()
    {
        foreach (var item in callbacks)
        {
            item();
        }
    }
}