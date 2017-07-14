using UnityEngine;
using System.Collections;

/// <summary>
/// 处理相机的跟随
/// </summary>
public class CameraFollowing : MonoBehaviour {

	public Transform target;//角色
	public float smoothing;//相机跟随平滑度

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position-target.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 newPosition = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, newPosition, smoothing * Time.deltaTime);
		}
	}
}
