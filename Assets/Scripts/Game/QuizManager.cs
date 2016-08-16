using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class QuizManager : MonoBehaviour {
	public QuizTimer timer;
	public QuizScore score;
	public Text quizText;
	public QuizBuilder quizBuilder;
	public PauseScreen pauseScreen;
	List<ImageToGuess> images;

	ImageToGuess correctImage;

	Dictionary<string, List<ImageToGuess>> matcherDictionary;

	ImageToGuessProvider imageToGuessProvider;

	// Use this for initialization
	void Start () {
		print (Time.time);
		imageToGuessProvider = ImageToGuessProvider.findInstance ();
		quizBuilder.imageChosenDelegate = this.imageWasChosen;
		List<ImageToGuess> unlockedImages = imageToGuessProvider.getUnlockedImages ();
		quizBuilder.imagesPrefabs = imageToGuessProvider.getRandomImagesFromList ((unlockedImages.Count + 8) / 2, unlockedImages);
		images = quizBuilder.build ();
		matcherDictionary = quizBuilder.getCopyHelperMatch ();
		updateImageToFind ();
		timer.reset ();
		score.resetTo (0);
	}

	// Update is called once per frame
	void Update () {
		if (timer.getValue() == 0) {
			int numberOfCoinsGained = this.score.getFinalScore () / 2;
			imageToGuessProvider.coins += numberOfCoinsGained;
			pauseScreen.setLoser (numberOfCoinsGained, imageToGuessProvider.coins);
			pauseGameAndReload ();
		}
	}

	void updateImageToFind() {
		this.correctImage = null;
		int search = Random.Range (0, matcherDictionary.Count);
		while (correctImage == null) { // FIXME: risk of infinite loop
			string hint = matcherDictionary.Keys.ElementAt(search);
			List<ImageToGuess> correctImages = matcherDictionary[matcherDictionary.Keys.ElementAt(search)];
			if (correctImages.Count == 1) {
				this.correctImage = correctImages [0];
				quizText.text = hint;
				break;
			}
			search = (search + 1) % matcherDictionary.Count;
		}
	}

	public void imageWasChosen(ImageToGuess chosenImage)
	{
		print("chosenImage: ");
		foreach (string hint in chosenImage.hints) {
			print(hint);
		}
		print("correctImage: ");
		foreach (string hint in correctImage.hints) {
			print(hint);
		}
		if (chosenImage == correctImage) {
			print ("success!");
			chosenImage.setInactiveSuccess ();
			score.addPointsAnimated (100);
		} else {
			print ("fail");
			chosenImage.setInactiveFail ();
			timer.decrementValue (1);
		}
		foreach (KeyValuePair<string,List<ImageToGuess>> matchingImages in matcherDictionary) {
			matchingImages.Value.Remove (chosenImage);
		}
		images.Remove (chosenImage);
		if (images.Count <= 1) {
			print ("number of coins: " + imageToGuessProvider.coins);
			imageToGuessProvider.coins += this.score.getFinalScore ();
			pauseScreen.setWinner (this.score.getFinalScore (), imageToGuessProvider.coins);
			pauseGameAndReload ();
		} else {
			updateImageToFind ();
		}
	}
	public void pauseGameAndReload() {
		this.quizBuilder.unbuild ();
		Start ();
		showPauseScreen ();
	}
	public void showPauseScreen() {
		pauseScreen.gameObject.SetActive (true);
		Time.timeScale = 0;
	}
	public void pauseGame() {
		showPauseScreen ();
		pauseScreen.setPaused();
	}
	public void unpauseGame() {
		pauseScreen.gameObject.SetActive (false);
		Time.timeScale = 1;
	}
	public void goToShop() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Collection");
	}
	public void endGame () {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Menu");
	}
}
