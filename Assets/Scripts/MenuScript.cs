using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("AsenTestscene");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
