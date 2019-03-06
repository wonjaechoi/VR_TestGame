using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour {

	void Awake() {

	}

	private int index;
	private float perfectTime;
	public void Init(int index, float perfectTime) {
		this.index = index;
		this.perfectTime = perfectTime;
	}

	void OnTriggerEnter(Collider c) {
		Debug.Log(((1 - Mathf.Abs(NoteMaker.globalTimer - perfectTime)) * 100) + "%");
		NoteMaker.instance.OnHitNote(this);
		UI_Manager.instance.txt_Accuracy.text = ((0.1f - Mathf.Abs(NoteMaker.globalTimer - perfectTime)) * 1000) + "%";
		Destroy(gameObject);
	}
}
