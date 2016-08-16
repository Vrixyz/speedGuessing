using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text), typeof(Shakeable), typeof(Animator))]
public class QuizTimer : MonoBehaviour {
	public float initialTimerValue = 10;
	public float animationDuration = 0.2f;

	float timer = 0;
	Text timerText;
	Shakeable shakeable;
	Animator animator;
	// Use this for initialization
	void Start () {
		timerText = this.GetComponent<Text> ();
		shakeable = this.GetComponent<Shakeable> ();
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0) {
			return;
		}
		timer -= Time.deltaTime;
		if (timer < 0) timer = 0; // clamp the timer to zero
		//int seconds = timer % 60; // calculate the seconds
		//int minutes = timer / 60; // calculate the minutes
		timerText.text = Mathf.CeilToInt(timer).ToString();
	}

	public void incrementValue(float valueToAdd) {
		this.timer += valueToAdd;
	}
	public void decrementValue(float valueToSubstract) {
		shakeable.shake (animationDuration);
		animator.speed = 1f / animationDuration;
		animator.SetTrigger ("showWrong");
		this.timer -= valueToSubstract;
		if (timer < 0) {
			timer = 0;
		}
	}
	public float getValue() {
		return this.timer;
	}
	public void reset() {
		this.timer = this.initialTimerValue;
	}
}
