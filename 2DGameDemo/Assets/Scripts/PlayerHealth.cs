using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 处理角色的血量是增加还是减少
/// </summary>
public class PlayerHealth : MonoBehaviour {

	public int health;
	public GameObject deathEffect;
	public RectTransform panel;

	private int currentHealth;
	private Transform[] healthImage;

	// Use this for initialization
	void Start () {
		currentHealth = health;
		GetHealthImage ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 减少血量
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void TakeDamage(int damage){
		currentHealth -= damage;
		foreach (Transform item in healthImage) {
			if (item.gameObject.activeInHierarchy) {
				item.gameObject.SetActive (false);
				break;
			}
		}
		if (currentHealth <= 0)
			Dead ();
	}

	/// <summary>
	/// 增加血量
	/// </summary>
	/// <param name="live">Live.</param>
	public void TakeHealth(int live){
		currentHealth += live;
		foreach (Transform item in healthImage) {
			if (!item.gameObject.activeInHierarchy) {
				item.gameObject.SetActive (true);
				break;
			}
		}
		if (currentHealth > health)
			currentHealth = health;
	}

	private void Dead(){
		Instantiate (deathEffect,transform.position,Quaternion.identity);
		Destroy (gameObject);
	}

	/// <summary>
	/// 获取角色血量的image数组
	/// </summary>
	private void GetHealthImage(){
		healthImage = new Transform[panel.childCount];
		for (int i = 0; i < panel.childCount; i++) {
			Transform temp = panel.GetChild (i);
			if (temp.tag == Tag.playerHealthImage)
				healthImage [i] = temp;
		}
	}
}
