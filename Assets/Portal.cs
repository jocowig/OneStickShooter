using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	private float timer = .80f;
	private float elapsed = 0;

	bool playerEntered = false;

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trap Collision Entered");
		if (other.tag.Equals("Player"))
		{
			AudioSource.PlayClipAtPoint (AudioManager.Instance.portal, this.transform.position);
			playerEntered = true;
		}
	}

	void Update() {
		if (playerEntered) {
			elapsed += Time.deltaTime;
			if (elapsed > timer) {
				BossEnemy.speed += Random.Range (.5f, 2f);
				PlayerScript.bossesKilled++;
				PlayerScript.maxHealth += Random.Range (2, 4);
				BossEnemy.maxHealth += Random.Range (5, 10);
				LevelGenerator.RoomsToGenerate += Random.Range (2, 6);
				SceneManager.LoadScene ("LoadingScreen");
			}
		}
	}
}
