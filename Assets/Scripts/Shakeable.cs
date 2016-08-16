using UnityEngine;
using System.Collections;

public class Shakeable : MonoBehaviour {
	Quaternion originalRotation = Quaternion.identity;
	float shakeTimeLeft = 0f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		
		if(shakeTimeLeft > 0)
		{
			float shakeAmount = 12;
			float shakeOffset = 0;
			float duration = 0.06f;
			float lerp = Mathf.PingPong (Time.time, duration) / duration;
			shakeOffset = Mathf.Lerp(-shakeAmount,shakeAmount,lerp);
			shakeTimeLeft -= Time.deltaTime;
			if (shakeTimeLeft <= 0) {
				this.transform.rotation = originalRotation;
			} else {
				this.transform.rotation = Quaternion.Euler (this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z + shakeOffset);
			}
		}
	}

	public void shake(float duration) {
		if (shakeTimeLeft <= 0) {
			originalRotation = this.transform.rotation;
		}
		shakeTimeLeft = duration;
	}
}
