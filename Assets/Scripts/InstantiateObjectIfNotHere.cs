using UnityEngine;
using System.Collections;

public class InstantiateObjectIfNotHere : MonoBehaviour {
	public GameObject prefab;
	public bool isReady = false;

	// Use this for initialization
	void Start () {
		if (GameObject.Find (prefab.name) == null) {
			GameObject go = Instantiate (this.prefab);
			go.name = prefab.name;
			this.isReady = true;
		}
	}

	void awake() {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void forceStart() {
		if (this.isReady == false) {
			Start ();
		}
	}
}
