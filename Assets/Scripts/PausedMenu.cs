using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour {

    public GameObject UI;
    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    public void Toggle()
    {
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
            Time.timeScale = 0f;
            //Time.fixedDeltaTime used for slow motion
        else
            Time.timeScale = 1f;
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

}
