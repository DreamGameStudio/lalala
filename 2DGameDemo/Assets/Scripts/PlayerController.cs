using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色的控制类，定义角色的移动，跳跃，射击，技能
/// </summary>
public class PlayerController : MonoBehaviour {

	public float speed;//水平移动速度
	public float jumpForce;//最大弹跳力
	public float radius;//检查地面的范围半径
	public Transform checkGround;//检查地面的中心点
	public LayerMask ground;//需要检查的层
	public LayerMask bushlayer;//可以碰撞弹射的层
	public float bashRadius;//检测可弹射的范围半径
	public float bashForce;//弹射力
	public float bushTime;//遇到碰撞点时暂停时间
	public Transform bashArrow;//弹射点指向箭头
	public bool isRewinding = false;//用来判断是否需要时光逆流
	public float recordTime;//时光逆流的时间
	public CollisionInformation state;
	public float shootTime;//射击间隔
	public GameObject projectile;//子弹
	public GameObject shootEfect;//射击时的特效
	public Transform gun;//枪口

	private float nextShootTime;
	private Rigidbody2D rb;
	Collider2D[] bashPoint;//存储可弹射点的信息
	private Animator anim;
	private bool isFacingRight;//用来判断是面朝那面
	private bool doubleJump;//判断是否可以二连跳
	private Vector2 bashDirection;//角色处于可弹射时的弹射方向
	private bool canBash;//用来判断是否可以弹射
	private List<CharacterInformation> characterInformations;
	private float originSpeed;
	private float buffContinueTime;

	void Awake(){
		state = new CollisionInformation ();
	}

	// Use this for initialization
	void Start () {
		originSpeed = speed;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		isFacingRight = true;
		doubleJump = false;
		characterInformations = new List<CharacterInformation> ();
	}

	void Update(){
		
		Bash ();
		Jump ();
		//按下shift开始时光倒流
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			StartRewind ();
		}
		//松开时停止
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			StopRewind ();
		}
		if (Input.GetMouseButtonDown (0)) {
			Shoot ();
		}
	}
		

	// Update is called once per frame
	void FixedUpdate () {
		
		if (isRewinding)
			Rewind ();
		else
			Record ();
		state.IsGround = Physics2D.OverlapCircle(checkGround.position,radius,ground);
		if (state.HasCollisions)
			doubleJump = false;
		anim.SetBool ("canJump",state.HasCollisions);
		anim.SetFloat ("velocityY",rb.velocity.y);
		Movement ();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,bashRadius);
	}
		
	/// <summary>
	/// 移动角色
	/// </summary>
	private void Movement(){
		/*if (!canBash) {
			float x = Input.GetAxis ("Horizontal");
			Flip (x);
			rb.velocity = new Vector2 (x * speed, rb.velocity.y);
			anim.SetFloat ("velocityX", Mathf.Abs (rb.velocity.x));
		}*/
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			float x = Input.GetAxis ("Horizontal");
			Flip (x);
			rb.velocity = new Vector2 (x * speed, rb.velocity.y);
			anim.SetFloat ("velocityX", Mathf.Abs (rb.velocity.x));
		}
		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
			anim.SetFloat ("velocityX", Mathf.Abs (rb.velocity.x));
		}
	}

	/// <summary>
	/// 反转角色
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	private void Flip(float x){
		if ((x > 0 && !isFacingRight)||(x < 0 && isFacingRight)) {
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = new Vector3 (newScale.x,newScale.y,newScale.z);
			isFacingRight = !isFacingRight;
		} 
	}

	/// <summary>
	/// 跳跃
	/// </summary>
	private void Jump(){
		if (state.HasCollisions&&Input.GetKeyDown (KeyCode.Space)) {
			state.Reset ();
			transform.position = new Vector3 (transform.position.x,transform.position.y+.3f,transform.position.z);
			rb.AddForce (new Vector2(0,jumpForce),ForceMode2D.Impulse);
			anim.SetBool ("canJump",state.HasCollisions);
		}
		//二连跳
		else if (!state.HasCollisions && !doubleJump && Input.GetKeyDown (KeyCode.Space)) {
			rb.velocity = new Vector2 (rb.velocity.x,0);
			rb.AddForce (new Vector2(0,jumpForce),ForceMode2D.Impulse);
			anim.SetBool ("canJump",doubleJump);
			doubleJump = true;
		}

	}

	/// <summary>
	/// 仿奥日弹射
	/// </summary>
	private void Bash(){
		//发现弹射点后按下鼠标右键暂停
		if (Input.GetMouseButtonDown (1)) {
			bashPoint = Physics2D.OverlapCircleAll (transform.position, bashRadius, bushlayer);
			if (bashPoint.Length != 0) {
				bashArrow.gameObject.SetActive (true);
				bashArrow.position = bashPoint [0].transform.position;
				StartCoroutine ("BushTimeWaiting");
				Time.timeScale = 0;
				canBash = true;
			}
		} 
		//鼠标右键弹起后弹射物体
		else if (Input.GetMouseButtonUp (1) && canBash) {
			bashArrow.gameObject.SetActive (false);
			Time.timeScale = 1;
			bashDirection = Camera.main.ScreenToWorldPoint (Input.mousePosition) - bashPoint [0].transform.position;
			bashDirection = bashDirection.normalized;
			Flip (bashDirection.x);
			transform.position = new Vector3 (bashPoint [0].transform.position.x + bashDirection.x, bashPoint [0].transform.position.y + bashDirection.y, transform.position.z);
			rb.velocity = Vector2.zero;
			bashDirection.y = bashDirection.y * 1.5f;
			bashDirection.x = bashDirection.x * 0.8f;
			rb.AddForce (bashDirection * bashForce, ForceMode2D.Impulse);
			StartCoroutine ("SetBashTime");
		} 
		//未弹射时箭头跟随鼠标选择
		else if (Input.GetMouseButton (1) && canBash) {
			Vector2 distance = Camera.main.ScreenToWorldPoint (Input.mousePosition) - bashPoint [0].transform.position;
			distance.Normalize ();
			float angle = Mathf.Atan2 (distance.y, distance.x) * Mathf.Rad2Deg;//得到鼠标与弹射点间的向量的角度
			bashArrow.transform.rotation = Quaternion.Euler (0,0,angle);
		}
	}

	/// <summary>
	/// 使用协程处理跟弹射点暂停的时间
	/// </summary>
	/// <returns>The time waiting.</returns>
	private IEnumerator BushTimeWaiting(){
		float waitTime = Time.realtimeSinceStartup + bushTime;
		while (Time.realtimeSinceStartup < waitTime) {
			yield return null;
		}
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			canBash = false;
			bashArrow.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// 弹射过程中不能在水平方向有移动
	/// </summary>
	/// <returns>The bash time.</returns>
	private IEnumerator SetBashTime(){
		float waitTime = Time.realtimeSinceStartup + 1;
		while (Time.realtimeSinceStartup < waitTime) {
			yield return null;
		}
		canBash = false;
	}

	/// <summary>
	/// 开始时光逆流
	/// </summary>
	private void StartRewind(){
		isRewinding = true;
		rb.isKinematic = true;//使角色不受力
	}

	/// <summary>
	/// 停止时光逆流
	/// </summary>
	private void StopRewind(){
		isRewinding = false;
		rb.isKinematic = false;//角色开始受力
	}

	/// <summary>
	/// 时光逆流
	/// </summary>
	private void Rewind(){
		//记录点数量大于0时才可以倒流
		if (characterInformations.Count >0) {
			CharacterInformation characterInformation = characterInformations [0];
			transform.position = characterInformation.position;
			transform.rotation = characterInformation.rotation;
			characterInformations.RemoveAt (0);
		}
	}

	/// <summary>
	/// 记录角色的信息
	/// </summary>
	private void Record(){
		if (characterInformations.Count > Mathf.Round (recordTime / Time.fixedDeltaTime)) {
			characterInformations.RemoveAt (characterInformations.Count-1);
		}
		characterInformations.Insert (0,new CharacterInformation(transform.position,transform.rotation));
	}

	/// <summary>
	/// 处理射击
	/// 
	/// </summary>
	private void Shoot(){
		if (Time.time < nextShootTime)
			return;
			anim.SetTrigger ("canShoot");
		if (isFacingRight) {
			Instantiate (projectile, gun.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		} else if (!isFacingRight) {
			Instantiate (projectile, gun.position, Quaternion.Euler (new Vector3 (0, 0, 180f)));
		}
		Instantiate (shootEfect,gun.position,Quaternion.identity);
		nextShootTime = Time.time + shootTime;
	}

	/// <summary>
	/// 处理受到伤害的效果
	/// </summary>
	/// <param name="force">Force.</param>
	public void GetHurt(Vector2 force){
		anim.SetTrigger ("canHit");
		rb.AddForce (-new Vector2(force.x*0.8f,force.y*1.2f),ForceMode2D.Impulse);
	}

	public void SpeedBuff(float newSpeed,float continueTime){
		speed = newSpeed;
		buffContinueTime = Time.time + continueTime;
		StartCoroutine ("ResetSpeed");
	}
		
	private IEnumerator ResetSpeed(){
		while (Time.time < buffContinueTime) {
			yield return null;
		}
		speed = originSpeed;
	}
		
}
