using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public Player Player;
    public float distance = 0.5f;
    public float ScanningHeight = 0.5f;
    public Action<CanCutObject> onCut;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float ScanningDistance = (transform.localScale.x / 2) + distance;
        Vector3 rayPosition;
        rayPosition = new Vector3(transform.position.x, transform.position.y + ScanningHeight, 0);
        Debug.DrawRay(rayPosition, transform.right * ScanningDistance, Color.blue);
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(rayPosition, transform.right, out hit, ScanningDistance))
            {
                Debug.Log("HITTED : " + hit.collider.gameObject.name);
                var cutObj = hit.collider.GetComponent<CanCutObject>();
                if (null != cutObj && !cutObj.isCut)
                {
                    if (!ItemSystem.instance.ItemUse(Item.cutter)) return;
                    cutObj.SendMessage("OnCut", Player);
                    onCut?.Invoke(cutObj);
                    return;
                }
            }
        }
    }
}
