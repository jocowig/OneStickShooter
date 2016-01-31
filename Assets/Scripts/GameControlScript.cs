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
        SceneManager.LoadScene("LoadingScreen");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
