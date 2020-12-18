using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    [Range(0, 10)]
    public int tutorialNum;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "Player")
        {
            TextShowUI.instance.TutorialShortShow(tutorialNum);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
