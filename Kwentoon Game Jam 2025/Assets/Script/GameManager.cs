using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI waveText;

    public int wave;
    public float coins;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
    }

    void UIUpdate()
    {
        coinsText.text = "Coins " + coins.ToString();
    }
}
