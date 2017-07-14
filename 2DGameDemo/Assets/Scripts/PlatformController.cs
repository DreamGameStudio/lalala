using UnityEngine;
using System.Collections;

/// <summary>
/// 控制平台按指定路线移动
/// </summary>
public class PlatformController : MonoBehaviour {

	public Vector3[] localPoints;//根据物体本地坐标得出的路径点的位置
	public bool cyclic;//用来判断是否需要环形云动
	[Range(0,2)]
	public float easeAmount;//平滑系数
	public float waitTime;//每个点等待的时间
	public float speed;//平台移动速度

	private float nextMoveTime;//到下一个点时需要等待的时间
	private int index;//当前点的索引
	private float percent;//移动增量
	private Vector3[] globalPoints;//世界坐标中路径点的位置

	// Use this for initialization
	void Start () {
		globalPoints = new Vector3[localPoints.Length];
		for (int i = 0; i < localPoints.Length; i++) {
			globalPoints [i] = localPoints [i]+transform.position;//将本地坐标转为世界坐标
		}
	}
	
	// Update is called once per frame
	void Update () {
		MovePlatform ();
	}

	void OnDrawGizmos(){
		if (localPoints != null) {
			Gizmos.color = Color.red;
			float size = .3f;
			for (int i = 0; i < localPoints.Length; i++) {
				Vector3 globalPosition = Application.isPlaying?globalPoints[i]:localPoints [i] + transform.position;
				Gizmos.DrawLine (globalPosition-Vector3.up*size,globalPosition+Vector3.up*size);
				Gizmos.DrawLine (globalPosition-Vector3.right*size,globalPosition+Vector3.right*size);
			}

		}
	}

	/// <summary>
	/// 移动平台
	/// </summary>
	private void MovePlatform(){
		if (Time.time < nextMoveTime)
			return;
		index %= globalPoints.Length;
		int nextIndex = (index + 1) % globalPoints.Length;
		float distance = Vector3.Distance (globalPoints[index],globalPoints[nextIndex]);
		percent += speed * Time.deltaTime / distance;
		percent = Mathf.Clamp01 (percent);
		float easePercent = Ease (percent);
		Vector3 newPos = Vector3.Lerp (globalPoints[index],globalPoints[nextIndex],easePercent);//得到每帧的位置
		//如果到达了下一个点
		if (percent >= 1) {
			percent = 0;
			index++;
			//如果不是环形运动
			if (!cyclic) {
				if (index >= globalPoints.Length - 1) {
					index = 0;
					System.Array.Reverse (globalPoints);//数组反转
				}
			}
			nextMoveTime = Time.time + waitTime;//重置等待时间
		}
		transform.Translate(newPos-transform.position,Space.World);
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
}
