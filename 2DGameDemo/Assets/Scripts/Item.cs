using UnityEngine;
using System.Collections;


/// <summary>
/// 道具类
/// </summary>
public enum ItemType{
	healthDrug,
	thunder,
	diamond,
	coin,
	enemy
}
public class Item : MonoBehaviour {

	public ItemType itemType;
	// Use this for initialization
	void Start () {
	
	}

	public virtual void GetEffect(GameObject player){

	}
}
