using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class QuizBuilder : MonoBehaviour {
	public delegate void ImageChosenDelegate(ImageToGuess num);
	public ImageChosenDelegate imageChosenDelegate;
	public List<ImageToGuess> imagesPrefabs;

	List<ImageToGuess> images;

	Dictionary<string, List<ImageToGuess>> imagesWordsDictionary;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<ImageToGuess> build() {
		imagesWordsDictionary = new Dictionary<string, List<ImageToGuess>> ();
		List<ImageToGuess> builtImages = new List<ImageToGuess> ();
		foreach (ImageToGuess prefab in imagesPrefabs) {
			GameObject o = Instantiate (prefab.gameObject);
			o.transform.SetParent (this.transform);
			ImageToGuess image = o.GetComponent<ImageToGuess>();
			// creates a helper to know easily which hint correspond to which image
			foreach (string hint in image.hints) {
				if (imagesWordsDictionary.ContainsKey(hint) == false) {
					imagesWordsDictionary.Add (hint, new List<ImageToGuess> ());
				}
				imagesWordsDictionary [hint].Add (image);
			}
			// hook the button
			Button button = o.GetComponent<Button> ();
			button.onClick.AddListener(() => imageChosenDelegate(image));
			builtImages.Add (image);
		}
		this.images = new List<ImageToGuess> (builtImages);
		return builtImages;
	}
	public void unbuild() {
		if (images == null) {
			return;
		}
		foreach (ImageToGuess image in images) {
			Destroy (image.gameObject);
		}
		images = null;
	}
	public Dictionary<string, List<ImageToGuess>> getCopyHelperMatch() {
		return new Dictionary<string, List<ImageToGuess>>(this.imagesWordsDictionary);
	}
}
