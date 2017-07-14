using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public static GameManager Instance{
		get{
			return _instance;
		}
	}

	public Text[] diamondTexts;
	[HideInInspector]
	public bool[] diamondFlag = new bool[5];

	// Use this for initialization
	void Awake () {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else if (_instance != this)
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDiamondText(int index){
		diamondTexts [index].text = ":1";
		diamondFlag [index] = true;
	}
}
