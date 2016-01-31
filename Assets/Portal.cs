using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trap Collision Entered");
		if (other.tag.Equals("Player"))
		{
			BossEnemy.speed += Random.Range (.5f, 2f);
			PlayerScript.bossesKilled++;
			PlayerScript.maxHealth += Random.Range (2, 4);
			BossEnemy.maxHealth += Random.Range (5, 10);
			LevelGenerator.RoomsToGenerate += Random.Range (2, 6);
			SceneManager.LoadScene ("LoadingScreen");
		}
	}
}
