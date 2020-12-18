using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Animator))]
public class WrinkleObject : MonoBehaviour
{
    protected Collider col;
    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }
}
