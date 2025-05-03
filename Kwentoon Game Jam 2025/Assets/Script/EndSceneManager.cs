using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{

    public float wavesSurvived;
    public float dmgDealt;
    public TextMeshProUGUI waveSurvivedText;
    public TextMeshProUGUI dmgDealtText;

    
    void Start()
    {

        dmgDealt = PlayerPrefs.GetFloat("Damage Dealth");
        wavesSurvived = PlayerPrefs.GetInt("Waves Survived");

        dmgDealtText.text = dmgDealt.ToString("0");
        waveSurvivedText.text = wavesSurvived.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
