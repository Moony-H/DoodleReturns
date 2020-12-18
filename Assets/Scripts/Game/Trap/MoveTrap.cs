using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MoveTrap : BaseTrap
{
    public float moveSpeed;
    public float timer = 0f;
    public Transform[] points;
    public int pointIdx = 0;
    
    private BehaviourJob moveJob;
    
    private void Start()
    {
        trapTrigger = GetComponent<Collider>();
        moveJob = BehaviourJob.Make(IMoveTrack(),true);
    }
    protected override void OnTrapIn(Collider other)
    {
        
    }

    protected override void OnTrapOut(Collider other)
    {
        
    }
    private IEnumerator IMoveTrack()
    {
        while(true)
        {
            if(pointIdx >= points.Length)
            {
                pointIdx = 0;
            }
            var nxtPointIdx = pointIdx + 1;
            if(nxtPointIdx >= points.Length)
                nxtPointIdx = 0;
            var curPoint = points[pointIdx].position;
            var nxtPoint = points[nxtPointIdx].position;
            var duration = (nxtPoint - curPoint).magnitude / moveSpeed;
            timer = 0f;
            while(timer <= duration)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(curPoint, nxtPoint, timer/duration);
                yield return null;
            }    
            ++pointIdx;
            yield return null;    
        }
    }
}
