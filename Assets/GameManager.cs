using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform leftHand_Tracker;
	public Transform rightHand_Tracker;

	public Transform leftHand_Object;
	public Transform rightHand_Object;

	//Start Game
	void Start() {
		StartCoroutine(HandTrackRoutine());
		GetComponent<NoteMaker>().StartGame();
	}

	private IEnumerator HandTrackRoutine() {
		while (true) {
			leftHand_Object.position = leftHand_Tracker.position;
			leftHand_Object.rotation = leftHand_Tracker.rotation;

			rightHand_Object.position = rightHand_Tracker.position;
			rightHand_Object.rotation = rightHand_Tracker.rotation;
			yield return null;
		}
	}
}
