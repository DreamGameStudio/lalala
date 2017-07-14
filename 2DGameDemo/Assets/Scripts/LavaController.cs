using UnityEngine;
using System.Collections;

/// <summary>
/// 处理岩浆流动和停止
/// </summary>
public class LavaController : MonoBehaviour {

	public float lavaTime;//从流动到停止或者停止到流动的间隔

	private float nextLavaTime;
	private float particleStartLifeTime;//记录粒子系统的粒子生命时间
	private ParticleSystem p;
	private bool isFLow;//用来判断是否流动
	private BoxCollider2D box;
	// Use this for initialization
	void Start () {
	    p = transform.FindChild ("LavaEffect").GetComponent<ParticleSystem> ();
		box = GetComponent<BoxCollider2D> ();
		particleStartLifeTime = p.startLifetime;
		isFLow = true;
		nextLavaTime = Time.time + lavaTime;
	}
	
	// Update is called once per frame
	void Update () {
		PlayLava ();
	}

	private void PlayLava(){
		if (Time.time < nextLavaTime)
			return;
		if (!isFLow) {//如果之前不是是流动的则重置粒子生命时间，开始流动
			p.startLifetime = particleStartLifeTime;
			StartCoroutine ("ReFlow");
			nextLavaTime = Time.time + lavaTime;
		}
		else if (isFLow) {//如果之前是流动的则使粒子生命时间为0，停止流动
			p.startLifetime = 0;
			StartCoroutine ("StopFlow");
			nextLavaTime = Time.time + lavaTime;
		}
	}

	/// <summary>
	/// 停止流动，因为停止流动需要些时间所以等几秒后才可通过
	/// </summary>
	/// <returns>The flow.</returns>
	private IEnumerator StopFlow(){
		float waitTime = 3.5f+Time.time;
		while (Time.time < waitTime) {
			yield return null;
		}
		box.enabled = false;
		isFLow = false;
	}

	private IEnumerator ReFlow(){
		float waitTime = 3.5f+Time.time;
		while (Time.time < waitTime) {
			yield return null;
		}
		box.enabled = true;
		isFLow = true;
	}
		
}
