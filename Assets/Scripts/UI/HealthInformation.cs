using TMPro;
using UnityEngine;

public class HealthInformation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Health playerHealth;
    private TextMeshProUGUI healthText;
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null) { 
            healthText.text = $"Health: {playerHealth.health.ToString()}";
        }
    }
}
