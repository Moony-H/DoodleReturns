using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : SingletonGameObject<JobManager> {

    private void OnApplicationQuit()
    {
        _instance = null;
    }
}
public class BehaviourJob
{
    public event Action<bool> OnComplete;
    private bool isRunning;
    private bool isPaused;
    public bool IsRunning => isRunning;
    public bool IsPaused => isPaused;

    private IEnumerator coroutine;
    private bool isJobKilled;
    private Stack<BehaviourJob> childJobStack;

    public Stack<BehaviourJob> JobStack => childJobStack;
    public IEnumerator Job => coroutine;

    public BehaviourJob(IEnumerator co, bool shouldStart = true)
    {
        coroutine = co;
        if (shouldStart)
            Start();
    }
    #region Static Funcs
    public static BehaviourJob Make(IEnumerator co, bool shouldStart = true)
    {
        return new BehaviourJob(co, shouldStart);
    }
    #endregion
    private IEnumerator DoWork()
    {
        while(IsRunning)
        {
            if (IsPaused)
                yield return null;
            else
            {
                if (coroutine.MoveNext())
                {
                    yield return coroutine.Current;
                }
                else
                {
                    if (null != childJobStack && childJobStack.Count > 0)
                    {
                        BehaviourJob childJob = childJobStack.Pop();
                        coroutine = childJob.coroutine;

                    }
                    else
                        isRunning = false;
                }
            }
        }
        OnComplete?.Invoke(isJobKilled);
        yield return null;
    }
    #region Public Funcs
    public BehaviourJob CreateAndAddChildJob(IEnumerator co)
    {
        var j = new BehaviourJob(co, false);
        AddChildJob(j);
        return j;
    }
    public void AddChildJob(BehaviourJob j)
    {
        if (null == childJobStack)
            childJobStack = new Stack<BehaviourJob>();
        childJobStack.Push(j);
    }
    public void RemoveChildJob(BehaviourJob childJob)
    {
        if (childJobStack.Contains(childJob))
        {
            var childStack = new Stack<BehaviourJob>(childJobStack.Count - 1);
            var allCurChildren = childJobStack.ToArray();
            Array.Reverse(allCurChildren);
            for (int i = 0; i < allCurChildren.Length; i++)
            {
                var j = allCurChildren[i];
                if (j != childJob)
                    childStack.Push(j);
            }
            childJobStack = childStack;
        }
    }
    public void Start()
    {
        isRunning = true;
        JobManager.Instance.StartCoroutine(DoWork());
    }
    public IEnumerator StartAsCoroutine()
    {
        isRunning = true;
        yield return JobManager.Instance.StartCoroutine(DoWork());
    }
    public void Pause()
    {
        isPaused = true;
        JobManager.Instance.StopCoroutine(coroutine);
    }
    public void UnPause()
    {
        isPaused = false;
        JobManager.Instance.StartCoroutine(coroutine);
    }
    public void Kill()
    {
        isJobKilled = true;
        isRunning = false;
        isPaused = false;
        JobManager.Instance.StopCoroutine(coroutine);
    }
    #endregion
}
