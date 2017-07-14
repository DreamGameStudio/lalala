using UnityEngine;
using System.Collections;

/// <summary>
/// 自动销毁物体
/// </summary>
public class AutoDestroy : MonoBehaviour {

	public float destroyTime;

	// Use this for initialization
	void Start () {
		Destroy (gameObject,destroyTime);
	}

}
