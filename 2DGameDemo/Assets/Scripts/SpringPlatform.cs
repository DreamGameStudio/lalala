using UnityEngine;
using System.Collections;

/// <summary>
/// 处理弹簧踏板
/// </summary>
public class SpringPlatform : MonoBehaviour {

	public float force;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void  OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == Tag.player) {
			anim.SetTrigger ("isHit");
		}
	}

	void  OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == Tag.player) {
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up*force,ForceMode2D.Impulse);
		}
	}
}
