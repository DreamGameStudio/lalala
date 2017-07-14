using UnityEngine;
using System.Collections;

public class ShootSpore : MonoBehaviour {

	public GameObject projectile;
	public Transform shootFrom;
	public float shootTime;

	private float nextShootTime;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		nextShootTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Shoot ();
	}

	void OnTriggerStay2D(Collider2D col){
		
	}

	private void Shoot(){
		if (nextShootTime < Time.time) {
			nextShootTime = Time.time + shootTime;
				Instantiate (projectile,shootFrom.position,Quaternion.identity);
				anim.SetTrigger ("canShoot");
		}
	}
}
