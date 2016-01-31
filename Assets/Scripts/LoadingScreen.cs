using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    IEnumerator Start()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("LevelGeneration");
        yield return async;
        Debug.Log("Loading complete");
    }

        // Update is called once per frame
        void Update () {
	
	}
}
