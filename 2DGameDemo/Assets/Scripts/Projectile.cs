using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public GameObject explosion;
	public int damage;
	public Vector2 hitForce;

	protected Rigidbody2D rb2D;

	// Use this for initialization
	protected virtual void Awake () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

}
