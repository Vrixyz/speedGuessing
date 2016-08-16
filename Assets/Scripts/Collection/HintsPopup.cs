using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HintsPopup : MonoBehaviour {
	public GameObject textPrefab;
	public List<string> hints;
	public delegate void PopupWasClosedDelegate();
	public PopupWasClosedDelegate popupWasClosedDelegate;
	public GridLayoutGroup hintsContainer;
	public Image Image;

	List<Text> texts;
	Vector3 containerOriginalPosition = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void build() {
		containerOriginalPosition = hintsContainer.transform.localPosition;
		texts = new List<Text> ();
		foreach (string text in hints) {
			Text hint = ((GameObject)Instantiate (textPrefab)).GetComponent<Text> ();
			hint.text = text;
			hint.transform.SetParent (hintsContainer.transform);
			texts.Add (hint);
		}
	}
	public void unbuild() {
		if (texts != null) {
			foreach (Text text in texts) {
				Destroy (text.gameObject);
			}
			texts = null;
			hintsContainer.transform.localPosition = containerOriginalPosition;
		}
	}
}
