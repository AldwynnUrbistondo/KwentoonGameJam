using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI waveText;

    public static int wave;

    public static bool isPause;
    public static bool hasLose;

    [Header("Coin Variables")]
    [HideInInspector] public float lerpCoins;
    public float coins;
    public float lerpDuration;
    private float lerpTime = 0f;

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

        CoinLerp();
    }

    void UIUpdate()
    {
        //coinsText.text = coins.ToString("0");
        waveText.text = "Wave: " + wave.ToString();
    }

    void SpeedUpTime()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 10;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }

    #region Coin Text Functions
    public void AddCoins(float addCoins)
    {
        coins += addCoins;
        lerpTime = 0;
        StartCoroutine(AddCoinColorChange());
        StartCoroutine(SizeLerp());
    }

    void CoinLerp()
    {
        if (lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime; // Increase lerp timer
            lerpCoins = Mathf.Lerp(lerpCoins, coins, lerpTime / lerpDuration); // Lerp from 0 to targetScore
            coinsText.text = lerpCoins.ToString("0");
        }
        else
        {
            coinsText.text = coins.ToString("0");
        }
    }

    IEnumerator AddCoinColorChange()
    {
        Color colorA = Color.black;
        Color colorB = Color.green;
        Color colorC = Color.white;
        float colorT = 0;

        while (colorT < 1)
        {
            colorT += Time.deltaTime / 0.2f;
            coinsText.color = Color.Lerp(colorC, colorB, colorT);
            yield return null;
        }

        colorT = 0;

        while (colorT < 1)
        {
            colorT += Time.deltaTime / 1f;
            coinsText.color = Color.Lerp(colorB, colorA, colorT);
            yield return null;
        }
    }

    IEnumerator SizeLerp()
    {
        float val = 1f;
        float sizeA = 1f;
        float sizeB = 1.25f;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / 0.1f;
            val = Mathf.Lerp(sizeA, sizeB, t);
            coinsText.transform.localScale = new Vector3(val, val, val);
            yield return null;
        }

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / 1f;
            val = Mathf.Lerp(sizeB, sizeA, t);
            coinsText.transform.localScale = new Vector3(val, val, val);
            yield return null;
        }
    }
    #endregion

}
