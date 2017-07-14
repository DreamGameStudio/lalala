using UnityEngine;
using System.Collections;

/// <summary>
/// 控制MotoEnemy的行为
/// </summary>
public class MotoEnemy : Enemy {

	public float attackRadius;//攻击范围
	public LayerMask attack;//攻击层
	public float attackTime;//攻击间隔
	public GameObject bullet;//子弹
	public float launchAngle;//子弹发射角度

	private float nextAttackTime;
	private bool isAttack;
	private float preX;//上一帧的x位置
	private float curX;//当前帧的x位置
	private bool isFacingRight;

	// Use this for initialization
	void Start () {
		currentHealth = health;
		healthBar.maxValue = health;
		healthBar.value = health;
	}
	
	// Update is called once per frame
	void Update () {
		isAttack = Physics2D.OverlapCircle (transform.position,attackRadius,attack);
		if (!isHit) {
			EnemyAI ();
		}
		preX = transform.position.x;
	}

	void LateUpdate(){
		curX = transform.position.x;
		Flip ();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,attackRadius);
	}

	public override void EnemyAI ()
	{
		//如果角色进入攻击范围则开始攻击
		if (isAttack) {
			if (Time.time < nextAttackTime)
				return;
			Vector3 newPosition = new Vector3 (transform.position.x,transform.position.y+2.2f,transform.position.z);
			//发射三枚子弹
			for (int i = 0; i < 3; i++) {
				MotoProjectile moto = ((GameObject)Instantiate (bullet,newPosition,Quaternion.identity)).GetComponent<MotoProjectile>();
				moto.SetForce (launchAngle*(i-1));
			}
			nextAttackTime = Time.time + attackTime;
		}
	}

	private void Flip(){
		if ((curX > preX && !isFacingRight) || preX > curX && isFacingRight) {
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = new Vector3 (newScale.x,newScale.y,newScale.z); 
			isFacingRight = !isFacingRight;
		}
	}
}
