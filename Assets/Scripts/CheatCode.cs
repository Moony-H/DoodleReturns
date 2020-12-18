using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Scene01");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Scene02");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Scene03");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Ending");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FindObjectOfType<ItemSystem>().ItemGet(Item.ink, 1000);
        }
    }
}
