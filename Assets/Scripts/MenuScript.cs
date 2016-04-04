using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
