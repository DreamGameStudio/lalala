using UnityEngine;
using System.Collections;


/// <summary>
/// 使物体漂浮
/// </summary>
public class Floating : MonoBehaviour {

	public float strengthFloating;

	private float originY;

	// Use this for initialization
	void Start () {
		originY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x,originY+Mathf.Sin(Time.time)*strengthFloating);
	}
}
