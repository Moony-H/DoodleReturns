using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Animator animator;
    public MoveController MoveController;
    public Hammering Hammering;
    public Cutting Cutting;
    public Grapping Grapping;
    public Transform GrapObject;
    public UnityEvent moveEvent, stopEvent, jumpEvent;
    public UnityEvent grabEvent, dropEvent, hammerEvent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        MoveController = GetComponent<MoveController>();
        Hammering = GetComponent<Hammering>();
        MoveController.onMove += OnMove;
        MoveController.onStop += OnStop;
        MoveController.onJump += OnJump;

        Hammering.onHammering += OnHammering;
        Grapping.onGrab += OnGrab;
        Grapping.onDrop += OnDrop;

        Cutting.onCut += OnCut;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var debri = hit.gameObject.GetComponent<DebriObject>();
        bool isNotDebri = (debri == null);
        //// rigidbody
        if (!isNotDebri)
        {
            //    // Calculate push direction from move direction,
            //    // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            //    // If you know how fast your character is trying to move,
            //    // then you can also multiply the push velocity by that.
            var pushPower = MoveController.GetVelocity.magnitude * 1.25f;
            // Apply the push
            debri.rigid.velocity = pushDir * pushPower;
        }
    }
    private void OnMove()
    {
        if(MoveController.controller.isGrounded)
            SoundManager.Instance.PlayStepSound();
        if (Input.GetAxis("Horizontal") > 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (Input.GetAxis("Horizontal") < 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        animator.SetBool("Running", true);
        moveEvent?.Invoke();
    }
    private void OnStop()
    {
        animator.SetBool("Running", false);
        stopEvent?.Invoke();
    }
    private void OnJump()
    {
        SoundManager.Instance.PlayJumpSound();
        jumpEvent?.Invoke();
    }
    private void OnGrab(CanGrabObject obj)
    {

        animator.SetBool("Grabbing", true);
        grabEvent?.Invoke();
    }
    private void OnDrop(CanGrabObject obj)
    {
        animator.SetBool("Grabbing", false);
        dropEvent?.Invoke();
    }
    private void OnHammering()
    {
        SoundManager.Instance.PlayHammerSound();
        hammerEvent?.Invoke();
    }
    private void OnCut(CanCutObject obj)
    {
        SoundManager.Instance.PlaySliceSound();
    }
}
