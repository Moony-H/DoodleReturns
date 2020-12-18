using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteractStart(GameObject actor, params object[] objs);
    void OnInteractUpdate(GameObject actor);
    void OnInteractEnd(GameObject actor);
}
public abstract class InteractObject : MonoBehaviour, IInteractable
{
    public enum State
     {
        STOP = 0,
        RUNNING
    }
    private GameObject _actor;
    public State interactState = State.STOP;
    [SerializeField]
    public Action<GameObject, object[]> onInteractStart;
    public Action<GameObject> onInteractUpdate;
    public Action<GameObject> onInteractEnd;

    public void Interact(GameObject actor, params object[] objs)
    {
        if(interactState == State.STOP)
        {
            interactState = State.RUNNING;
            _actor = actor;
            onInteractStart?.Invoke(actor, objs);
        }
    }
    public void StopInteract(GameObject actor)
    {
        if(interactState == State.RUNNING) onInteractEnd?.Invoke(actor);
        actor = null;
        interactState = State.STOP;
    }
    protected virtual void Start() 
    {
        onInteractStart += OnInteractStart;
        onInteractUpdate += OnInteractUpdate;
        onInteractEnd += OnInteractEnd;
    }
    private void Update()
    {
        if(interactState == State.RUNNING)
        {
            onInteractUpdate?.Invoke(_actor);
        }
    }

    public abstract void OnInteractStart(GameObject actor, params object[] objs);
    public abstract void OnInteractUpdate(GameObject actor);
    public abstract void OnInteractEnd(GameObject actor);
}
