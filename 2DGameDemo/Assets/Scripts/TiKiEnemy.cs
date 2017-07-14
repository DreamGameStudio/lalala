using UnityEngine;
using System.Collections;

/// <summary>
/// 具体的敌人类,简单的ai行为
/// </summary>
public class TiKiEnemy : Enemy {

	public float speed;

	private Rigidbody2D rb;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		healthBar.maxValue = health;
		healthBar.value = health;
		resetTime = hitTime;
		currentHealth = health;
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		direction = (transform.position.x - target.transform.position.x > 0) ? Vector2.left : Vector2.right;
	}
	
	// Update is called once per frame
	void Update () {
		EnemyAI ();
	}

	public override void EnemyAI (){
		if (!isHit) {
			rb.velocity = direction * speed;
		}
	}
		
}
