using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class QuizScore : MonoBehaviour {
	public int score = 0;
	public float incrementDuration = 0.5f;
	int targetScore = 0;
	int realScore = 0;
	float elapsedTime = 0;
	Text scoreText;
	// Use this for initialization
	void Start () {
		scoreText = this.GetComponent<Text> ();
		resetTo (0);
	}
	
	// Update is called once per frame
	void Update () {
		int scoreDifference = this.targetScore - this.score;

		if (scoreDifference != 0) {
			elapsedTime += Time.deltaTime;
			this.realScore = this.score + (int)(scoreDifference * (elapsedTime / this.incrementDuration));
			this.scoreText.text = this.realScore.ToString ();
			print (targetScore);
			if (elapsedTime >= this.incrementDuration) {
				this.realScore = this.score = this.targetScore;

			}
			this.scoreText.text = this.realScore.ToString ();
		}
	}

	public void addPointsAnimated(int scoreToAdd) {
		this.score = this.realScore;
		this.elapsedTime = 0;
		targetScore = this.targetScore + scoreToAdd;
	}
	public void resetTo (int score) {
		this.score = score;
		this.targetScore = score;
		this.realScore = score;
		this.elapsedTime = 0;
		this.scoreText.text = this.realScore.ToString ();
	}
	public int getFinalScore() {
		return targetScore;
	}
}
