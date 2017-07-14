using UnityEngine;
using System.Collections;

/// <summary>
/// 存放道具的盒子类
/// </summary>
public class Box : MonoBehaviour {

	public GameObject explosion;


	// Use this for initialization
	void Start () {
		
	}

	/// <summary>
	/// 当被子弹击中后，出现爆照效果，并在此随机生成道具
	/// </summary>
	public void GetHit(){
		Instantiate (explosion,transform.position,Quaternion.identity);
		ItemController.Instance.CreateItem (transform.position);
		Destroy (gameObject);
	}
}
