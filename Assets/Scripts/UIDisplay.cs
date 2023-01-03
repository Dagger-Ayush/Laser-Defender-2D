using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerhealth;

    [Header("Score")]

    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        healthSlider.maxValue = playerhealth.GetCurrentHealth();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        healthSlider.value = playerhealth.GetCurrentHealth();
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("000000000");
    }
}
