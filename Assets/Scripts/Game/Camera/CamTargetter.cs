using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamTargetter : MonoBehaviour
{
    public float maxDis;
	public Transform 		mainFollow;
	public List<Transform> 	followGroup;	//	No Contain mainFollow
	public List<float> 		followWeight;	//	Each Element can be 0 ~ 1 value

	private Vector3 dir;
	private float dis;
	private float camRatio = 16f / 9f;
	public float maxViewSize = 7f;
	public CinemachineVirtualCamera targetCam;
	// Use this for initialization
	void Start () 
	{		
		if (followGroup.Count > followWeight.Count)
			followWeight.AddRange (new float[followGroup.Count - followWeight.Count]);
		else if (followGroup.Count < followWeight.Count)
			followWeight.RemoveRange (followGroup.Count - 1, followWeight.Count - followGroup.Count);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 sumPos = Vector3.zero;
		if (followGroup.Count != 0) 
		{
			for (int i = 0; i < followGroup.Count; i++) 
			{
				sumPos += Vector3.Lerp (mainFollow.transform.position, followGroup [i].transform.position, followWeight [i]);
			}
			sumPos /= followGroup.Count;
		} 
		else
			sumPos = mainFollow.transform.position;

		dir = (sumPos - mainFollow.position).normalized;
		dis = Vector3.Distance (mainFollow.position, sumPos);

		Debug.DrawLine (sumPos, sumPos + dir * dis);

		if (dis > maxDis) {
			transform.position = mainFollow.position + dir * maxDis;
			var newViewSize = maxDis + (dis - maxDis) * 0.5f;
			newViewSize = Mathf.Min (maxViewSize, newViewSize);
			SetCameraViewSize (newViewSize);
		} else {
			transform.position = sumPos;
		}
	}
	public void SetCameraViewSize (float camViewSize)
	{
		targetCam.m_Lens.OrthographicSize = camViewSize;
	}
	public void AddFollowElement (Transform e)
	{
		if (!followGroup.Contains (e))
			followGroup.Add (e);
	}
	public void EditFollowWeight (int idx, float weight)
	{
		followWeight [idx] = weight;
	}

	public void RemoveFollowElement (Transform e)
	{
		followGroup.Remove (e);
	}
	public void RemoveFollowElement (int i)
	{
		followGroup.RemoveAt (i);
	}
}
