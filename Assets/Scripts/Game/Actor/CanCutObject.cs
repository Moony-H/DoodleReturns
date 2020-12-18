using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CanCutObject : MonoBehaviour
{
    public GameObject refHidedObj;
    public GameObject[] refDebriObjs;
    public Animator animator;
    public Collider trigger;
    public bool isCut = false;
    public float explosionPower;
    public string cutAnim;

    // Start is called before the first frame update
    private void Start() 
    {
        //animator = GetComponent<Animator>();
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
        if(null != refHidedObj)
            refHidedObj.SetActive(false);    
        for (var i = 0; i < refDebriObjs.Length; i++)
        {
            refDebriObjs[i].SetActive(false);
        }
    }
    public void OnCut(Player player)
    {
        if(isCut) return;
        animator.Play(cutAnim);
        if(null != refHidedObj)
            refHidedObj?.SetActive(true);
        trigger.enabled = false;
        for(var i = 0; i < refDebriObjs.Length; i++)
        {
            var instance = Instantiate(refDebriObjs[i], transform.position, Quaternion.identity);
            instance.SetActive(true);
            var tRigid = instance.GetComponent<Rigidbody>();
            if(null == tRigid) continue;
            var dir = (instance.transform.position - player.gameObject.transform.position).normalized;
            tRigid.AddForce(explosionPower * dir, ForceMode.Impulse);
        }
        gameObject.SetActive(false);
    }
    
}
