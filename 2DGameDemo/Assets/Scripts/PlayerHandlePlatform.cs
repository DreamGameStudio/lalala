using UnityEngine;
using System.Collections;

/// <summary>
/// 处理角色跟平台的碰撞检测
/// </summary>
public class PlayerHandlePlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == Tag.movingPlatform)
			transform.parent.parent = col.gameObject.transform;
		if (col.gameObject.tag == Tag.box) {
			transform.parent.GetComponent<PlayerController> ().state.IsStandingPlatform = true;
		}
	}
		

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == Tag.movingPlatform)
			transform.parent.parent = null;
		if (col.gameObject.tag == Tag.box)
			transform.parent.GetComponent<PlayerController> ().state.IsStandingPlatform = false;
	}
}
