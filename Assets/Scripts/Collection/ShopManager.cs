using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour {
	public Image unlockImageDisplay;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator runImageTeaserAnimation(List<ImageToGuess> lockedImages, ImageToGuess unlockedImage, Shakeable button) {
		while (lockedImages.Count > 0) {
			ImageToGuess imageToTease = lockedImages [Random.Range (0, lockedImages.Count)];
			print("displaying: " + imageToTease.hints[0]);
			unlockImageDisplay.sprite = imageToTease.GetComponent<Image>().sprite;
			yield return new WaitForSeconds(0.1f / lockedImages.Count);
			lockedImages.Remove (imageToTease);
		}
		print("not here ?");
		unlockImageDisplay.sprite = unlockedImage.GetComponent<Image>().sprite;
		print("unlockedImage: " + unlockedImage.hints[0]);
		Rewardable imageRewadable = unlockImageDisplay.GetComponent<Rewardable> ();
		if (imageRewadable) {
			imageRewadable.reward (0.2f);
		}
		button.gameObject.SetActive (true);
		return true;
	}

	public void buyButtonClicked(Shakeable buttonClicked) {
		ImageToGuessProvider imageToGuessProvider = ImageToGuessProvider.findInstance ();
		if (imageToGuessProvider.coins >= 1000) {
			List<ImageToGuess> lockedImages = imageToGuessProvider.getLockedImages ();
			if (lockedImages.Count <= 0) {
				Shakeable imageShakeable = unlockImageDisplay.GetComponent<Shakeable> ();
				if (imageShakeable) {
					imageShakeable.shake (0.2f);
				}
				print ("you unlocked everything!");
				return;
			}
			ImageToGuess unlockedImage = lockedImages [Random.Range (0, lockedImages.Count)];
			unlockedImage.unlocked = true;
			imageToGuessProvider.coins -= 1000;
			lockedImages.Remove (unlockedImage);
			StartCoroutine(runImageTeaserAnimation(lockedImages, unlockedImage, buttonClicked));
		} else {
			buttonClicked.shake (0.3f);
			print ("not enough coins!");
		}
	}
}
