using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Story_Ending : MonoBehaviour
{
    public VideoPlayer video;
    void Start()
    {
        // DefaultUI.instance.OpenScene("Credit");
        //SceneManager.LoadScene("Credit");
    }

    void Update()
    {
        if (!video.isPlaying)
        {
            SceneManager.LoadScene("Credit");
        }
    }

}
