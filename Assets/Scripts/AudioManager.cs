using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioClip music;
	public static AudioSource musicSource;

	// Use this for initialization
	void Start () {
		if (AudioManager.music == null) {
			AudioManager.music = Resources.Load("music.ogg") as AudioClip;
		}

		if (AudioManager.musicSource == null) {
			musicSource = gameObject.AddComponent<AudioSource> ();
			musicSource.clip = AudioManager.music;
		}

		if (!AudioManager.musicSource.isPlaying) {
			AudioManager.musicSource.Play ();
			AudioManager.musicSource.loop = true;
		}

		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
