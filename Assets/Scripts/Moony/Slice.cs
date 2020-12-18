using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Slice : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public List<GameObject> cutobjlist;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slicing()
    {
        if (!ItemSystem.instance.ItemUse(Item.cutter)) return;
        for(int i = 0; i<cutobjlist.Count; i++)
        {
            Instantiate(cutobjlist[i], transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySliceSound();
        GetComponent<Slice>().enabled = false;
    }

}
