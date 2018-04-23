using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///LoadScene , used to control scenes andor quit d

public class Scene  : MonoBehaviour
{
    // Use this for initialization
    bool mytimeScale = true;
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


}