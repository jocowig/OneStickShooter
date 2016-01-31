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
			LevelGenerator.RoomsToGenerate += Random.Range (2, 6);
			SceneManager.LoadScene ("LevelGeneration");
		}
	}
}
