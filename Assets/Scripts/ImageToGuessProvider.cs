using UnityEngine;
using System.Collections.Generic;

public class ImageToGuessProvider : MonoBehaviour {
	public int coins = 100;
	public List<ImageToGuess> images;

	Dictionary<string, List<ImageToGuess>> imagesForHint;

	static ImageToGuessProvider instance;
	public static ImageToGuessProvider findInstance() {
		return ImageToGuessProvider.instance;
	}

	void Awake() {
		ImageToGuessProvider.instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		print ("ImageToGuessProvider");
		ImageToGuessProvider.instance = this;
		updateImageList (this.images);
	}

	public void updateImageList (List<ImageToGuess> images) {
		this.images = images;
		imagesForHint = new Dictionary<string, List<ImageToGuess>> ();
		if (this.images == null) {
			return;
		}
		foreach (ImageToGuess image in this.images) {
			foreach (string hint in image.hints) {
				if (imagesForHint.ContainsKey (hint) == false) {
					imagesForHint.Add (hint, new List<ImageToGuess> ());
				}
				imagesForHint [hint].Add (image);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public List<ImageToGuess> getRandomImagesFromList(int count, List<ImageToGuess> elligibleImages) {
		List<ImageToGuess> imagesToReturn = new List<ImageToGuess>();

		while (imagesToReturn.Count < count && 0 < elligibleImages.Count) {
			// FIXME: getting from hint dictionnary is safer to obtain unique combinations
			ImageToGuess randomImage = elligibleImages [Random.Range (0, elligibleImages.Count)];
			imagesToReturn.Add (randomImage);
			elligibleImages.Remove (randomImage);
		}
		return imagesToReturn;
	}

	public List<ImageToGuess> getUnlockedImages() {
		List<ImageToGuess> unlockedImages = new List<ImageToGuess>();
		foreach (ImageToGuess image in this.images) {
			if (image.unlocked) {
				unlockedImages.Add (image);
			}
		}
		return unlockedImages;
	}
	public List<ImageToGuess> getLockedImages() {
		List<ImageToGuess> lockedImages = new List<ImageToGuess>();
		foreach (ImageToGuess image in this.images) {
			if (image.unlocked == false) {
				lockedImages.Add (image);
			}
		}
		return lockedImages;
	}
}
