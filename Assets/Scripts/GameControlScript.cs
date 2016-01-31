using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchToGameScreen()
    {
        SceneManager.LoadScene("LevelTest");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
