using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public float showTime;
	public GameObject[] diamond;
	public AudioClip showAudioClip;

	private float nextShowTime;
	private int index;
	private int count;
	private int diamondCount;
	private bool isFinish;
	private bool[] diamondFlag;

	// Use this for initialization
	void Start () {
		index = 0;
		diamondCount = diamond.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFinish) {
			Finish ();
			isFinish = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == Tag.player) {
			GetDiamond ();
			for (int i = 0; i < diamondFlag.Length; i++) {
				if (diamondFlag [i]) {
					diamond [i].SetActive (true);
					count++;
					break;
				}
				index++;
			}
			nextShowTime = Time.time + showTime;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (!isFinish) {
			if (col.tag == Tag.player) {
				if (Time.time < nextShowTime)
					return;
				for (index=index+1;  index< diamondFlag.Length; index++) {
					if (diamondFlag [index]) {
						diamond [index].SetActive (true);
						count++;
						break;
					}
				}
				nextShowTime = Time.time + showTime;
				if (count == diamondCount) {
					isFinish = true;
					index = 0;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == Tag.player) {
			foreach (GameObject item in diamond) {
				item.SetActive (false);
			}
			index = 0;
			count = 0;
		}
	}

	private void Finish(){
		print ("Finish");
	}

	private void GetDiamond(){
		diamondFlag = GameManager.Instance.diamondFlag;
	}
}
