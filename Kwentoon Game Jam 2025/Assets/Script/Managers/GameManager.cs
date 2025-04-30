using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI waveText;

    public static int wave;
    public float coins;

    public static bool isPause;
    public static bool hasLose;

    void Start()
    {
        isPause = false;
        hasLose = false;
        wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();

        if (!isPause)
        {
            SpeedUpTime();
        }
    }

    void UIUpdate()
    {
        coinsText.text = coins.ToString("0");
        waveText.text = "Wave: " + wave.ToString();
    }

    void SpeedUpTime()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 4;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }
}
