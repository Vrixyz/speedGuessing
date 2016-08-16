using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreen : MonoBehaviour {

	public Text flavorText;
	public Button continueButton;
	public Button backButton;
	public Button shopButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPaused() {
		flavorText.text = "Paused";
		continueButton.GetComponentInChildren<Text>().text = "Continue";
		backButton.GetComponentInChildren<Text>().text = "Menu";
	}
	void hideEveryButtons() {
		continueButton.gameObject.SetActive (false);
		backButton.gameObject.SetActive (false);
		shopButton.gameObject.SetActive (false);
	}
	public void setWinner(int coinsGained, int currentCoins) {
		flavorText.text = "Winner ! you gain " + coinsGained + " coins. You now have " + currentCoins + " coins";
		hideEveryButtons ();
		if (currentCoins >= 1000) {
			shopButton.gameObject.SetActive (true);
			shopButton.GetComponentInChildren<Text> ().text = "You got enough gold to buy a new image !!!";
		} else {
			continueButton.gameObject.SetActive (true);
			backButton.gameObject.SetActive (true);
			continueButton.GetComponentInChildren<Text>().text = "I can do it again!";
			backButton.GetComponentInChildren<Text>().text = "Menu";
		}
	}
	public void setLoser(int coinsGained, int currentCoins) {
		hideEveryButtons ();
		continueButton.gameObject.SetActive (true);
		backButton.gameObject.SetActive (true);
		flavorText.text = "No time left! you only get half your points in coins: " + coinsGained + " ; you now have " + currentCoins + " coins";
		continueButton.GetComponentInChildren<Text>().text = "I will do better!";
		backButton.GetComponentInChildren<Text>().text = "Menu";
	}

}
