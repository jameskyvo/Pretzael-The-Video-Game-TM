using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverCanvas;
    public GameObject victoryCanvas;

    private GameObject player;
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void Victory()
    {
        victoryCanvas.SetActive(true);

        if (player != null)
        {
            player.SetActive(false);
        }
    }
}
