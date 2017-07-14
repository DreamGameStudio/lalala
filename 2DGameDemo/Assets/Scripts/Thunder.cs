using UnityEngine;
using System.Collections;

public class Thunder : Item {

	public float speed;
	public float continueTime;

	private float time;

	public override void GetEffect (GameObject player)
	{
		player.GetComponent<PlayerController> ().SpeedBuff (speed,continueTime);
		Destroy (gameObject);
	}
		
}
