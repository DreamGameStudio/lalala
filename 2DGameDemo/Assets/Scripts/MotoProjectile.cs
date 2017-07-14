using UnityEngine;
using System.Collections;

/// <summary>
/// 控制motoenemy的子弹
/// </summary>
public class MotoProjectile : Projectile {

	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetForce(float angle){
		transform.rotation = Quaternion.AngleAxis (angle,transform.forward);
		rb2D.AddForce (transform.up*speed,ForceMode2D.Impulse);//瞬间力
	}
}
