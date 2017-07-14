using UnityEngine;
using System.Collections;

/// <summary>
/// 处理当角色进入障碍物范围时自动掉落
/// </summary>
public class SpikeTopTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == Tag.player) {
			transform.parent.GetComponent<Rigidbody2D> ().isKinematic = false;
			transform.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (transform.parent.gameObject,5f);
		}
	}
}
