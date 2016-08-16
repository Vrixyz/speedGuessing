using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CollectionManager : MonoBehaviour {
	public QuizBuilder builder;
	public HintsPopup hintsPopup;
	public Text coinText;

	public ImageToGuess selectedImage;
	public InstantiateObjectIfNotHere dependency;
	public ShopManager shopManager;

	enum State {Collection, Hints, Shop};
	State state = State.Collection;

	ImageToGuessProvider imageToGuessProvider;

	IEnumerator WaitForSecondsWrapper(float secs)
	{
		yield return new UnityEngine.WaitForSeconds(secs);
	}

	// Use this for initialization
	void Start () {
		print ("CollectionManager start");
		dependency.forceStart ();

		print ("CollectionManager ended waiting.");
		imageToGuessProvider = ImageToGuessProvider.findInstance ();
		if (imageToGuessProvider.coins >= 1000) {
			shopButtonClicked ();
		}
		builder.imageChosenDelegate = this.imageWasChosen;
		reloadCollection ();
	}

	void reloadCollection() {
		builder.unbuild ();
		builder.imagesPrefabs = imageToGuessProvider.getUnlockedImages ();
		builder.build ();
		coinText.text = imageToGuessProvider.coins + " coins";
	}

	void didLoadProvider() {
		this.gameObject.SetActive (true);

	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void imageWasChosen(ImageToGuess chosenImage)
	{
		this.state = State.Hints;
		if (selectedImage != null) {
			selectedImage.resetStatus ();
		}
		chosenImage.setInactiveSuccess ();
		selectedImage = chosenImage;
		print("chosenImage: ");
		hintsPopup.hints = chosenImage.hints;
		hintsPopup.Image.sprite = selectedImage.GetComponent<Image> ().sprite;
		hintsPopup.build ();
		hintsPopup.gameObject.SetActive(true);
	}
	public void shopButtonClicked() {
		if (selectedImage != null) {
			backButton ();
		}
		this.state = State.Shop;
		shopManager.gameObject.SetActive (true);
	}

	public void backButton() {
		print ("state: "  + state.ToString ());
		if (state == State.Hints) {
			this.state = State.Collection;
			selectedImage.resetStatus ();
			selectedImage = null;
			hintsPopup.gameObject.SetActive (false);
			hintsPopup.unbuild ();
		} else if (state == State.Shop) {
			this.state = State.Collection;
			reloadCollection ();
			shopManager.gameObject.SetActive (false);
		} else {
			SceneManager.LoadScene ("Menu");
		}
	}
}
