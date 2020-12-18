using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammering : MonoBehaviour
{
    public Player Player;
    public Action onHammering;
    public Animator animator;
    public float HammeringTime = 0.4f;
    private float CoTime;

    private int boxIndex;
    // Start is called before the first frame update
    void Start()
    {
        CoTime = HammeringTime;
        Player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        boxIndex = 1;
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("Grabbing")&& ItemSystem.instance.ItemUse(Item.hammer))
        {
            onHammering?.Invoke();
            CoTime = HammeringTime;
            animator.SetTrigger("Hammer_trig");
            //StartCoroutine("Hammer");
            if(Player.transform.localRotation.y < 0){
                boxIndex = -1;
            }else if(Player.transform.localRotation.y >= 0){
                boxIndex = 1;
            }
            Collider[] colliderArray = Physics.OverlapBox(transform.position + new Vector3(boxIndex, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), Quaternion.identity);
            for (int i = 0; i < colliderArray.Length; i++)
            {
                if(colliderArray[i].CompareTag("Spring"))
                    colliderArray[i].SendMessage("Bending", Player);
                //colliderArray[i].SendMessage("OnHammering", Player);
            }
            Debug.Log("Hammered");

        }
        
    }
    IEnumerator Hammer()
    {
        Collider[] colliderArray = Physics.OverlapBox(transform.position + new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 0f), transform.rotation);
        while (CoTime > 0)
        {
            for (int i = 0; i < colliderArray.Length; i++)
            {
                if(colliderArray[i].CompareTag("Spring"))
                    colliderArray[i].SendMessage("OnHammering", Player);
            }
            CoTime -= 0.1f;
            Debug.Log("Hammered");
            yield return new WaitForSeconds(0.1f);
        }
        animator.SetBool("Hammer", false);
        
    }
}
