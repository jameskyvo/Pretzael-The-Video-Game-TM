using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    private int clickDelay = 1;
    private bool isFading = false;

    public void StartGame()
    {
        if (isFading == true)
        {
            return;
        }

        StartCoroutine(FadeToStart());
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public IEnumerator FadeToStart()
    {
        isFading = true;
        yield return new WaitForSeconds(clickDelay);
        SceneManager.LoadScene("Game");
    }
}
