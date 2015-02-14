using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public AudioClip alpha;
	public AudioClip beta;
	public AudioClip theta;
	public AudioClip gamma;
	public AudioClip delta;

	private List<AudioClip> clips;
	private List<float> volocity;
	private List<AudioSource> waves;

	//private AudioSource alpah;

	private float volumeTimer;

	// Use this for initialization
	void Start () {

		waves = new List<AudioSource> ();
		volocity = new List<float>();
		clips = new List<AudioClip> ();

		clips.Add (alpha);
		clips.Add (beta);
		clips.Add (theta);
		clips.Add (gamma);
		clips.Add (delta);

		for (int i = 0; i < clips.Count; ++i) {
			volocity.Add (0.0f);
		}

		for (int i = 0; i < clips.Count; ++i) {
			waves.Add (gameObject.AddComponent<AudioSource>());
			waves[i].clip = clips[i];
			waves[i].loop = true;
			waves[i].volume = 0.5f;
			waves[i].Play ();
				}
	}
	
	// Update is called once per frame
	void Update () {
		volumeTimer += Time.deltaTime;
		if (volumeTimer > 3.0f) {
			volumeTimer -= 3.0f;
			for (int i = 0; i < clips.Count; ++i) {
				volocity [i] = WaveData.FFTdata [i] / 10.0f - waves [i].volume;
				waves[i].pitch = (Random.Range (0, 10) % 2) + 1.0f;
			}
		}
		else if(volumeTimer < 1.0f)
		{
			for (int i = 0; i < clips.Count; ++i) {
				waves[i].volume += volocity[i] * Time.deltaTime;
			}
		}
	}
}
