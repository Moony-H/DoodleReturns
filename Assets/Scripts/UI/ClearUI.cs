using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    public static ClearUI instance;

    public GameObject bgobj;
    // public Animator bgobjAni;
    // public Text objtext;

    public void Awake()
    {
        instance = this;
        //bgobjAni = GetComponent<Animator>();
    }

    public void ClearUIOpen(bool _open = true)
    {
        bgobj.SetActive(_open);
    }

    public void ClearUIShut()
    {
        ClearUIOpen(false);
    }

}
