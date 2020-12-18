using System.Collections.Generic;
using UnityEngine;

public class RayCollisionDetector2D : MonoBehaviour
{
    public Collider target;
    public float space = 0.1f;
    public LayerMask collisionMask;
    public float minTestDistance = 0.01f;
    public float skinWidth = 0.005f;
    public float multiplier = 2f;
    public bool debug = false;

    private Vector3 center;
    private Vector3 extent;
    private int chopX;
    private int chopY;

    //  0 : All Side, 1 : Above, Below, 2 : Right, Left Side
    public Color debugColor = Color.magenta;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (null == target)
            target = GetComponent<Collider>();
        extent = target.bounds.extents + Vector3.one * skinWidth;
        center = target.bounds.center;
        chopX = (int)(target.bounds.size.x / space);
        chopY = (int)(target.bounds.size.y / space);
    }
    public void Update()
    {
        center = target.bounds.center;
    }

    public List<RaycastHit> TestSide(Vector3 velocity)
    {
        var signX = Mathf.Sign(velocity.x);
        var speedX = velocity.x * signX * multiplier;
        speedX = Mathf.Max(speedX, minTestDistance);
        var result = new List<RaycastHit>();
        for (var c = 0; c < chopY + 1; c++)
        {
            var dir = signX * Vector3.right;
            var pos = center;
            pos.x += signX * extent.x;
            pos.y += extent.y - space * c;
            var hits = Physics.RaycastAll(pos, dir, speedX, collisionMask.value);
            result.AddRange(hits);
            if (debug)
            {
                Debug.DrawLine(pos, pos + dir * speedX, debugColor);
            }
        }
        return result;
    }

    public List<RaycastHit> TestBottomTop(Vector3 velocity)
    {
        var signY = Mathf.Sign(velocity.y);
        var speedY = velocity.y * signY * multiplier;
        speedY = Mathf.Max(speedY, minTestDistance);
        var result = new List<RaycastHit>();
        for (var c = 0; c < chopX + 1; c++)
        {
            var dir = signY * Vector3.up;
            var pos = center;
            pos.x += space * c - extent.x;
            pos.y += signY * extent.y;
            var hits = Physics.RaycastAll(pos, dir, speedY, collisionMask.value);
            result.AddRange(hits);
            if (debug)
            {
                Debug.DrawLine(pos, pos + dir * speedY, debugColor);
            }
        }

        return result;
    }
    public List<RaycastHit> TestAllSide(Vector3 velocity)
    {
        var result = TestSide(velocity);
        result.AddRange(TestBottomTop(velocity));
        return result;
    }
}
