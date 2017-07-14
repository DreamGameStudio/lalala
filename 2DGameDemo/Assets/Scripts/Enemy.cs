using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 敌人的基类,定义敌人的血量，接收伤害，死亡，ai的函数
/// </summary>
public class Enemy : MonoBehaviour {

	public int health;
	public float hitTime;
	public GameObject deathEffect;
	public Slider healthBar;//生命条

	protected int currentHealth;
	protected bool isHit;
	protected float resetTime;
	protected GameObject target;


	public void TakeDamage(int damage){
		healthBar.gameObject.SetActive (true);
		isHit = true;
		resetTime = Time.time + hitTime;
		StartCoroutine ("ResetHit");
		currentHealth -= damage;
		healthBar.value = currentHealth;
		if (currentHealth <= 0)
			Dead ();
	}

	protected void Dead(){
		Instantiate (deathEffect,transform.position,Quaternion.identity);
		Destroy (gameObject);
	}

	public virtual void EnemyAI(){

	}

	protected IEnumerator ResetHit(){
		while (Time.time < resetTime) {
			yield return null;
		}
		isHit = false;
	}
}
