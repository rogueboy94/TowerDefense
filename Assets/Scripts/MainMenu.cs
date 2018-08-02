using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string levelToLoad;

    public SceneFader sceneFader;

    void Start()
    {
        levelToLoad = "MainScene";
    }
	public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
