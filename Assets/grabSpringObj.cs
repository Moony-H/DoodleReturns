using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabSpringObj : MonoBehaviour
{
    Animator grabAnimator;
    SpringObject springobj;

    bool bend = false;
    // Start is called before the first frame update
    void Start()
    {
        grabAnimator = GetComponent<Animator>();
        springobj = GetComponentInChildren<SpringObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(springobj.isBending == true){
            
            grabAnimator.SetInteger("parentbendCount",1);
            bend = true;
        }else if(springobj.isBending == false && bend){
            grabAnimator.SetInteger("parentbendCount",0);    
            grabAnimator.SetTrigger("parentunbend");
            bend = false;
        }
    }
}
