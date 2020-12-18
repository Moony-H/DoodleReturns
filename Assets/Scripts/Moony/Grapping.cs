using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapping : MonoBehaviour
{
    public Player Player;
    public CanGrabObject grabbedObject;
    public float distance = 0.5f;
    public float ScanningHeight = 0.5f;
    public Action<CanGrabObject> onGrab, onDrop;
    private bool bendbool = true;
    
    private bool curgrab = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        bendbool = true;
        RaycastHit hit;
        float ScanningDistance = (transform.localScale.x / 2) + distance;
        Vector3 rayPosition;
        rayPosition = new Vector3(transform.position.x, transform.position.y + ScanningHeight, 0);
        Debug.DrawRay(rayPosition, transform.right * ScanningDistance, Color.red);
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(rayPosition, transform.right, out hit, ScanningDistance) && !curgrab)
            {
                if (hit.collider != null)
                {
                    if(hit.transform.GetComponentInChildren<SpringObject>()!=null)
                    {
                        bendbool = hit.transform.GetComponentInChildren<SpringObject>().isBending;
                    }

                    Debug.Log(hit.collider.gameObject.name);
                    grabbedObject = hit.collider.GetComponent<CanGrabObject>();
                    if (null != grabbedObject && !grabbedObject.isGrab&&bendbool)
                    {
                        grabbedObject.SendMessage("OnGrab", Player);
                        curgrab = true;
                        onGrab?.Invoke(grabbedObject);
                        return;
                    }
                }
            }
            if(null != grabbedObject && grabbedObject.isGrab)
            {
                grabbedObject.SendMessage("OnDrop", Player);
                curgrab = false;
                onDrop?.Invoke(grabbedObject);
                grabbedObject = null;
            }
        }

    }
    
}
