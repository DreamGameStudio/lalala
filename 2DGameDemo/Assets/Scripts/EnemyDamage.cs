using UnityEngine;
using System.Collections;

/// <summary>
/// 处理的敌人对角色的伤害
/// </summary>
public class EnemyDamage : MonoBehaviour {

	public bool isDestroy;
	public int damgae;
	public float hurtForce;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == Tag.player) {
			if (transform.tag == Tag.spikeTop)
				gameObject.layer = LayerMask.NameToLayer (Tag.spikeTop);
			Vector2 direction = col.contacts [0].normal;
			direction.Normalize ();
			col.gameObject.GetComponent<PlayerHealth> ().TakeDamage (damgae);
			col.gameObject.GetComponent<PlayerController> ().GetHurt (direction*hurtForce);
			if (isDestroy)
				Destroy (gameObject);
		}
	}

}
