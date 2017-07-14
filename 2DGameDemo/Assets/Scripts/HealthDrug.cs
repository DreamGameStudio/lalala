using UnityEngine;
using System.Collections;


/// <summary>
/// 具体的item类，药品，用于增加角色的血量
/// </summary>
public class HealthDrug : Item {

	public int bounsHealth;

	// Use this for initialization
	void Start () {
	
	}

	public override void GetEffect (GameObject player)
	{
		player.GetComponent<PlayerHealth> ().TakeHealth (bounsHealth);
		Destroy (gameObject);
	}
}
