using UnityEngine;
using System.Collections;

/// <summary>
/// 处理水对角色的伤害
/// </summary>
public class WaterDamage : MonoBehaviour {

	public int damage;
	public float damageTime;

	private float nextDamageTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.tag == Tag.player) {
			if (Time.time < nextDamageTime)
				return;
			col.transform.parent.GetComponent<PlayerHealth> ().TakeDamage (damage);
			col.transform.parent.GetComponent<PlayerController> ().GetHurt (Vector2.zero);
			nextDamageTime = Time.time + damageTime;
		}
	}
}
