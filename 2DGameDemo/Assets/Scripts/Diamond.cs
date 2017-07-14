using UnityEngine;
using System.Collections;

public class Diamond : Item {

	public int index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void GetEffect (GameObject player)
	{
		GameManager.Instance.SetDiamondText (index);
		Destroy (gameObject);
	}
}
