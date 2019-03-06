using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMaker : MonoBehaviour {
	public static NoteMaker instance;
	void Awake() {
		instance = this;
	}

	public GameObject prefab_Note;
	public new AudioSource audio;
	public ParticleSystem effect;

	public void StartGame() {
		StartCoroutine(GameRoutine());
		StartCoroutine(MusicPlayRoutine());
	}

	private int BPM = 106;

	private float distance = 50;			//N Unit
	private float humanArmOffset = 0.3f;	//사람 팔길이고려
	private float noteSpeed = 10;			//N Unit per Second
	private float timeOffset_Distance = 5;  //Second
	private float timeOffset_Sync = 1.25f;

	private List<NoteObject> notes = new List<NoteObject>();


	private IEnumerator MusicPlayRoutine() {
		yield return new WaitForSeconds(timeOffset_Distance);
		audio.Play();
	}

	public static float globalTimer = 0;
	private IEnumerator GameRoutine() {
		globalTimer = 0;
		float nextBeatGenerateTime = timeOffset_Sync;
		int index = 0;

		while (true) {
			globalTimer += Time.deltaTime;
			if (nextBeatGenerateTime <= globalTimer) {
				Debug.Log(index);
				notes.Add(Instantiate(prefab_Note, new Vector3(Random.Range(-1f, 1f), 2 + Random.Range(-1f, 1f), distance + humanArmOffset + (globalTimer - nextBeatGenerateTime) * noteSpeed), Quaternion.identity).GetComponent<NoteObject>());
				notes[notes.Count - 1].Init(index, nextBeatGenerateTime + timeOffset_Distance);
				index++;
				nextBeatGenerateTime = CalcBeatTime(index) + timeOffset_Sync;
			}

			for(int i = notes.Count-1; i >= 0; i--) {
				if (notes[i] == null) {
					notes.RemoveAt(i);
					continue;
				}
				notes[i].transform.Translate(0, 0, -Time.deltaTime * noteSpeed);
				if (notes[i].transform.position.z < -1) {
					NoteObject temp = notes[i];
					notes.RemoveAt(i);
					Destroy(temp.gameObject);
				}
			}
			yield return null;
		}
	}

	private void EndMusic() {

	}

	private float CalcBeatTime(int index) {
		float interval = 60f / BPM;
		float result = 0;
		while (index >= BPM) {
			index -= BPM;
			result += 60;
		}
		result += interval * index;
		return result;
	}

	public void OnHitNote(NoteObject noteObject) {
		effect.transform.position = noteObject.transform.position;
		effect.Emit(30);
	}
}
