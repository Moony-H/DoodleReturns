using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CanGrabObject : MonoBehaviour
{
    public float throwPower = 200f;
    public class GrabParam
    {
        public GameObject actor;
        public Transform pivot;
        
        public GrabParam(GameObject a, Transform p)
        {
            actor = a;
            pivot = p;
        }
    }
    protected Rigidbody rigid;
    public Collider col;
    protected Transform curPivot = null;
    protected Transform restoreParent;
    public bool isGrab = false;

    // Start is called before the first frame update
    protected void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if(col == null){
            col = GetComponent<Collider>();
        }
        restoreParent = transform.parent;

    }
    public static GrabParam GetGrabParam(GameObject actor, Transform pivot = null) => new GrabParam(actor, pivot); 
    public void OnGrab(Player player)
    {
        
        if (transform.childCount!=0)
        {
            transform.GetChild(0).GetComponent<SpringObject>().isGrapping = true;
            if(!transform.GetChild(0).GetComponent<SpringObject>().isBending)
            {
                return;
            }
        }
        var grabParam = GetGrabParam(player.gameObject, player.GrapObject);
        var actor = grabParam.actor;
        var pivot = grabParam.pivot;
        
        

        if(pivot != null)
            curPivot = pivot;
        else
            curPivot = actor.transform;
        transform.parent = curPivot;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.EulerAngles(new Vector3(0,0,0));
        rigid.velocity = Vector3.zero;
        rigid.Sleep();
        col.isTrigger = true;
        isGrab = true;
    }
    public void OnDrop(Player player)
    {
        
        if (transform.childCount != 0)
        {
            transform.GetChild(0).GetComponent<SpringObject>().isGrapping = false;
        }
        float DropDir = 0;
        if (player.transform.localRotation.y < 0)
        {
            DropDir = -throwPower;
        }
        else if (player.transform.localRotation.y >= 0)
        {
            DropDir = throwPower;
        }
        
        isGrab = false;
        transform.SetParent(restoreParent);
        curPivot = null;
        rigid.WakeUp();
        rigid.velocity = Vector3.zero;
        Debug.Log("Drop: " + DropDir.ToString());
        col.isTrigger = false;
        rigid.AddForce(new Vector3(DropDir, throwPower, 0));
    }
    private void FixedUpdate()
    {
        if (isGrab)
        {
            transform.localPosition = Vector3.zero;
            rigid.Sleep();
        }

    }
}
