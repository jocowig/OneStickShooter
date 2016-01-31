using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	
	private static AudioManager instance = null;
	public static AudioManager Instance { get { return instance; } }

	public AudioClip music;
	public AudioClip portal;
	public AudioClip[] fireworks;
	public AudioClip[] enemies;
	public AudioClip boss;

	void Awake() {
		instance = this;
		GetComponent<AudioSource> ().clip = music;
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		Play();
	}

	public void Play() {
		if (!GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().Play ();
		}
	}

	public void Stop() {
		GetComponent<AudioSource> ().Stop ();
	}

	public AudioClip GetRandomFirework(){
		fireworks.Shuffle ();
		return fireworks [0];
	}

	public AudioClip GetRandomEnemy() {
		enemies.Shuffle ();
		return enemies [0];
	}
}
