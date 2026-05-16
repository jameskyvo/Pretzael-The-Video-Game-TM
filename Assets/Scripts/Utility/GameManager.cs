using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager instance;

    public GameObject gameOverCanvas;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }
}
