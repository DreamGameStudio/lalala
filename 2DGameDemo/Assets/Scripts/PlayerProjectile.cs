using UnityEngine;
using System.Collections;

/// <summary>
/// 控制玩家子弹
/// </summary>
public class PlayerProjectile : Projectile {


	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();
		rb2D.AddForce (transform.right*speed,ForceMode2D.Impulse);//瞬间力
	}

	public void RemoveForce(){
		rb2D.velocity = new Vector2 (0,0);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == Tag.box) {
			col.GetComponent<Box> ().GetHit ();
		}
		if (col.tag == Tag.Enemy) {
			Instantiate (explosion,transform.position,Quaternion.identity);
			col.GetComponent<Enemy> ().TakeDamage (damage);
			//col.GetComponent<Rigidbody2D> ().AddForce (hitForce);
		}
		Destroy (gameObject);
	}
}
