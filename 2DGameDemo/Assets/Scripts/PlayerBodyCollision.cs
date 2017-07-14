using UnityEngine;
using System.Collections;

public class PlayerBodyCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 处理角色遇到道具的效果
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D(Collider2D col){
		if (col.GetComponent<Item> () != null) {
			col.GetComponent<Item> ().GetEffect (transform.parent.gameObject);
		}
	}
}
