using UnityEngine;
using System.Collections;

/// <summary>
/// 控制炮台的子弹
/// </summary>
public class CannonProjectile : Projectile {

	protected override void Awake ()
	{
		base.Awake ();
		rb2D.AddForce (transform.up*speed,ForceMode2D.Impulse);//瞬间力
	} 
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == Tag.player) {
			col.gameObject.GetComponent<PlayerHealth> ().TakeDamage (damage);
			col.gameObject.GetComponent<PlayerController> ().GetHurt (Vector2.zero);
		}
		//Instantiate (explosion,transform.position,Quaternion.identity);
		Destroy (gameObject);
	}
}
