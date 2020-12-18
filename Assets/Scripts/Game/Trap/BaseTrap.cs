using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ITrapReceiver
{
    void OnTrapped(BaseTrap trap);
}

public abstract class BaseTrap : MonoBehaviour
{
    public Collider trapTrigger;
    public List<Collider> victimCaches = new List<Collider>();

    [Header("Trap In Event")]
    public UnityEvent onTrapIn;
    [Header("Trap Out Event")]
    public UnityEvent onTrapOut;

    // Start is called before the first frame update
    void Start()
    {
        trapTrigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            var cached = victimCaches.Find(x => x == other);
            if (null != cached) return;
            victimCaches.Add(other);
            onTrapIn?.Invoke();
            OnTrapIn(other);
            var receiver = other.GetComponent<ITrapReceiver>();
            receiver?.OnTrapped(this);
        }
        
    }
    private void OnTriggerExit(Collider other) 
    {
        victimCaches.Remove(other);
        onTrapOut?.Invoke();
        OnTrapOut(other);
    }
    protected abstract void OnTrapIn(Collider other);
    protected abstract void OnTrapOut(Collider other);
}
