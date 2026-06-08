using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class ButtonLogic : MonoBehaviour
{
    private int clickDelay = 1;
    private bool isFading = false;
    [SerializeField]
    private EventReference buttonClick;

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
        if (isFading == true)
        {
            return;
        }

        Application.Quit();
    }

    public void ReturnToMenu()
    {
        if (isFading == true)
        {
            return;
        }

        SceneManager.LoadScene("Title");
    }

    public IEnumerator FadeToStart()
    {
        isFading = true;
        RuntimeManager.PlayOneShot(buttonClick);
        yield return new WaitForSeconds(clickDelay);
        SceneManager.LoadScene("Game");
    }
}
