using UnityEngine;
using System.Collections;

public class Rewardable : MonoBehaviour {
	Vector3 originalScale = Vector3.one;
	float scaleDuration = 1f;
	float scaleTimeLeft = 0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(scaleTimeLeft > 0)
		{
			float scaleAmount = 0.4f;
			float scaleOffset = 0;
			float lerp = Mathf.PingPong (scaleTimeLeft, scaleDuration) / scaleDuration;
			scaleOffset = Mathf.Lerp(-scaleAmount,scaleAmount,lerp);
			scaleTimeLeft -= Time.deltaTime;
			if (scaleTimeLeft <= 0) {
				this.transform.localScale = originalScale;
			} else {
				this.transform.localScale = new Vector3 (this.transform.localScale.x + scaleOffset, this.transform.localScale.y + scaleOffset, this.transform.localScale.z + scaleOffset);
			}
		}
	}

	public void reward(float duration) {
		if (scaleTimeLeft <= 0) {
			originalScale = this.transform.localScale;
		}
		scaleTimeLeft = duration;
		scaleDuration = duration;
	}
}
