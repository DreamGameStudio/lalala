using UnityEngine;
using System.Collections;

public class TriangleMoveColider : MonoBehaviour {
	private Transform Tf;
	private TriangleMove TM;
	public float speed=0; //移动速度
	public float speedOverrige=2;//速度倍率
	private int flag=0;

	// Use this for initialization
	void Awake()
	{
		transform.parent = null;
	}
	void Start () {
		Tf = GameObject.FindGameObjectWithTag (Tag.cog).GetComponent<Transform> ();
		TM = GameObject.FindGameObjectWithTag (Tag.cog).GetComponent<TriangleMove> ();
		ColliderTransform ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void ColliderTransform()
	{	
		float Py = Tf.position.y;
		float Px = Tf.position.x;
		float Sy = Tf.localScale.y;
		float Sx = Tf.localScale.x;
		this.transform.localScale = new Vector3 (8 * Sy + TM.w, 8 * Sy + TM.h, 0);
		this.transform.position = new Vector3 (Px+TM.w/2, Py - TM.h/2,0);
		
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == Tag.player&&flag==0) 
		{
			speed = speed * speedOverrige;
		}
		flag = 1;
	}
	void  OnTriggerExit2D(Collider2D col){
		if (col.tag == Tag.player) {
			speed = speed / speedOverrige;
		}
		flag = 0;
	}
}
