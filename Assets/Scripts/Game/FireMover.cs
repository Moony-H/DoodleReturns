using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreSystem.Additional;

public class FireMover : MonoBehaviour
{
    public NoiseSettings moveNoiser;
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 pos;
        Quaternion ori;
        moveNoiser.GetSignal(timer, out pos, out ori);
        transform.localPosition = pos;
        transform.localRotation = ori;
    }
}
