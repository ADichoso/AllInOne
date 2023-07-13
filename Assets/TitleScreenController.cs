using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreenController : MonoBehaviour
{
    public GameObject HelpPanel;

    bool canClick = true;

    public void OnStartButton()
    {
        if (canClick)
        {
            SceneManager.LoadScene(1);
            canClick = false;
        }
    }
    public void OnHelpButton()
    {
        if (canClick)
        {
            HelpPanel.SetActive(true);

            canClick = false;
        }
    }
    public void OnQuitButton()
    {
        if (canClick)
        {
            Application.Quit();
        }
    }

    public void OnHelpPanelClose()
    {
        canClick = true;
        HelpPanel.SetActive(false);
    }
}
