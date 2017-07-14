using UnityEngine;
using System.Collections;

public class TriangleMove : MonoBehaviour {
	private  int flag = 0;
	private float position_x;//获取初始位置坐标
	private float position_y;
	public float h = 5;//上下移动范围
	public float w =8;//左右西东范围
	public TriangleMoveColider tmc;

	// Use this for initialization
	void Awake()
	{
		position_x = transform.position.x;
		position_y = transform.position.y;
	}
	void Start () {
		tmc = GameObject.FindGameObjectWithTag (Tag.collider1).GetComponent<TriangleMoveColider> ();
	}

	// Update is called once per frame
	void f1()
	{
		if(flag==0)
			transform.position = Vector2.MoveTowards (transform.position, new Vector2(position_x,position_y-h), Time.deltaTime * tmc.speed);
		if (transform.position.y == position_y-h)
			flag = 1;
	}
	void f2()
	{
		if(flag==1)
			transform.position = Vector2.MoveTowards (transform.position, new Vector2(position_x+w,position_y-h), Time.deltaTime * tmc.speed);
		if (transform.position.x == position_x+w)
			flag = 2;
	}	
	void f3()
	{
		if(flag==2)
			transform.position = Vector2.MoveTowards (transform.position, new Vector2(position_x,position_y), Time.deltaTime * tmc.speed);
		if (transform.position.y == position_y)
			flag = 0;
	}

	void Update () {
		f1 ();
		f2 ();
		f3 ();
	}

}
 