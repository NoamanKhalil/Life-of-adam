using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///LoadScene , used to control scenes andor quit d

public class Scene  : MonoBehaviour
{
    
    // Use this for initialization
    bool mytimeScale = true;
    public GameObject aboutPanel;
    public GameObject mainPanel;
    public void LoadScene(string str)
    {
        SceneManager.LoadScene(str);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void pause()
    {

        Time.timeScale = 0;
    }
    public void unPause()
    {
        Time.timeScale = 1;
    }

    public void ToVideoScene()
    {
        SceneManager.LoadScene("VideoScene");
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void AboutPanel()
    {
        aboutPanel.gameObject.SetActive(true);
        mainPanel.gameObject.SetActive(false);
    }

    public void MainPanel()
    {
        mainPanel.gameObject.SetActive(true);
        aboutPanel.gameObject.SetActive(false);
    }



    


}