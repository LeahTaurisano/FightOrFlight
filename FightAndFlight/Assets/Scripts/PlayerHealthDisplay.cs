using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] PlayerController player;

    [SerializeField] private TextMeshProUGUI meterText;

    void Update()
    {
        healthText.text = "Health: " + player.GetHealth().ToString();
        meterText.text = "Meter: " + player.GetMeter().ToString("F0");
    }
}
