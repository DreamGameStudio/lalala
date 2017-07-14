using UnityEngine;
using System.Collections;


/// <summary>
/// 全局道具控制类，用于在指定位置随机生成道具
/// </summary>
public class ItemController : MonoBehaviour {

	private static ItemController _instance;

	public static ItemController Instance{
		get{
			return _instance;
		}
	}

	public GameObject[] item;

	// Use this for initialization
	void Awake () {
		if (_instance == null)
			_instance = this;
		else if (_instance != this) {
			Destroy (gameObject);
		}
	}


	public void CreateItem(Vector3 pos){
		int index = Random.Range (0,item.Length);
		Instantiate (item[index],pos,Quaternion.identity);
	}
}
