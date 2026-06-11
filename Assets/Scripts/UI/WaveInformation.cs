using TMPro;
using UnityEngine;

public class WaveInformation : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public TextMeshProUGUI waveText;

    private int currentWave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = waveSpawner.currentWave + 1;
        waveText.text = $"Wave: {currentWave.ToString()} / {waveSpawner.maxWaves}";
    }
}
