using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent (typeof (Image), typeof (Button), typeof(Shakeable))]
[RequireComponent (typeof (Rewardable))]
public class ImageToGuess : MonoBehaviour {
	public List<string> hints;
	public bool unlocked = true;
	Image image;
	Shakeable shakeable;
	Rewardable rewardable;

	// Use this for initialization
	void Start () {
		shakeable = this.GetComponent<Shakeable> ();
		rewardable = this.GetComponent<Rewardable> ();
		image = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setInactiveFail() {
		shakeable.shake (0.2f);
		image.color = Color.red;
	}

	public void setInactiveSuccess() {
		rewardable.reward (0.2f);
		image.color = Color.green;
	}

	public void resetStatus() {
		image.color = Color.white;
	}
}
