using UnityEngine;
using System.Collections;

public enum FlyingEnemyState{
	normalPatrol,
	flying,
	attack,
	hit
}
/// <summary>
/// 控制飞行敌人的行为
/// </summary>
public class FlyingEnemy : Enemy {

	public FlyingEnemyState state;//敌人的状态
	public float normalPatrolTime;//正常巡逻切换间隔
	public float flyingSmoothing;//由攻击转为飞回原位置时的飞行平滑度
	public float attackWaitTime;//一次完整的攻击时间
	public float attackSpeed;//攻击时飞行的速度
	[Range(0,2)]
	public float easeAmount;//平滑系数：攻击时飞行的
	public float attackRadius;//攻击范围
	public LayerMask attackLayer;//攻击层

	private float nextAttackTime;
	private float attackPercent;//攻击时移动增量
	private float nextnormalPatrolTime;
	private Animator anim;
	private Vector3 originPosition;//记录敌人初始时的位置
	private Vector3 targetPosition;//记录目标的位置
	private Vector3 currentPosition;//记录攻击时敌人的位置
	private bool isFacingRight;//判断敌人朝向哪面

	// Use this for initialization
	void Start () {
		healthBar.maxValue = health;
		healthBar.value = health;
		currentHealth = health;
		anim = GetComponent<Animator> ();
		state = FlyingEnemyState.normalPatrol;
		originPosition = transform.position;
		target = GameObject.FindGameObjectWithTag ("Player");
		isFacingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isHit)
			CheckPlayer ();
		else
			state = FlyingEnemyState.hit;
		EnemyAI ();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,attackRadius);
	}

	public override void EnemyAI (){
		//左右摆动巡逻
		if (state == FlyingEnemyState.normalPatrol) {
			if (Time.time < nextnormalPatrolTime)
				return;
			Flip ();
			nextnormalPatrolTime = Time.time+normalPatrolTime;
		}
		//返回原始点
		else if (state == FlyingEnemyState.flying) {
			Flying ();
		}else if (state == FlyingEnemyState.attack) {
			Attack ();
		} else if (state == FlyingEnemyState.hit) {
			anim.SetTrigger ("isHit");
			state = FlyingEnemyState.flying;
		}
	}

	/// <summary>
	/// 切换方向
	/// </summary>
	private void Flip(){
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = new Vector3 (newScale.x,newScale.y,newScale.z); 
		    isFacingRight = !isFacingRight;
	}

	/// <summary>
	/// 处理从攻击转为飞行时飞回原点
	/// </summary>
	private void Flying(){
		transform.position = Vector3.Lerp (transform.position, originPosition, flyingSmoothing * Time.deltaTime);
		if (Mathf.Abs (transform.position.x - originPosition.x) <= .1f)
			state = FlyingEnemyState.normalPatrol;
	}

	/// <summary>
	/// 处理攻击行为
	/// </summary>
	private void Attack(){
		if (Time.time < nextAttackTime)
			return;
		float distance = Vector2.Distance (target.transform.position,transform.position);
		if ((target.transform.position.x - transform.position.x) > 0 && !isFacingRight)
			Flip ();
		else if ((target.transform.position.x - transform.position.x) < 0 && isFacingRight)
			Flip ();
		currentPosition = transform.position;
		targetPosition = target.transform.position;
		StartCoroutine (Attacking(distance));
		nextAttackTime = Time.time + attackWaitTime;
	}

	/// <summary>
	/// 攻击中
	/// </summary>
	/// <param name="distance">Distance.</param>
	private IEnumerator Attacking(float distance){
		while (attackPercent < 1) {
			attackPercent += attackSpeed * Time.deltaTime / distance;
			attackPercent = Mathf.Clamp01 (attackPercent);
			float easePercent = Ease (attackPercent);
			Vector3 newPos = Vector3.Lerp (currentPosition,targetPosition,easePercent);//得到每帧的位置
			newPos = newPos-transform.position;
		
			transform.Translate(new Vector3(newPos.x,newPos.y,0),Space.World);
			yield return null;
		}
		attackPercent = 0;
	}

	/// <summary>
	/// 平滑处理
	/// y=xa/xa+(1-x)a
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	private float Ease(float x){
		float a = easeAmount + 1;
		return Mathf.Pow (x, a) / (Mathf.Pow (x, a) + Mathf.Pow (1 - x,a));
	}

	/// <summary>
	/// 检测目标是否进入攻击范围
	/// </summary>
	private void CheckPlayer(){
		bool isPlayer = Physics2D.OverlapCircle (transform.position,attackRadius,attackLayer);
		if (isPlayer)
			state = FlyingEnemyState.attack;
		else if(Mathf.Abs(transform.position.x-originPosition.x)<=0.1f)
			state = FlyingEnemyState.normalPatrol;
		else state = FlyingEnemyState.flying;
	}
}
