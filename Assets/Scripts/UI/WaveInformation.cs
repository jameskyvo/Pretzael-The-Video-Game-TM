using TMPro;
using UnityEngine;

public class WaveInformation : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public TextMeshProUGUI waveText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = $"Wave: {waveSpawner.currentWave.ToString()}";
    }
}
