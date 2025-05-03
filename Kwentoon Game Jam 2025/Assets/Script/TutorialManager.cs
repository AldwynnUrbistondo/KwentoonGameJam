using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    float holdWASD = 0;
    float holdFire = 0;
    bool wasdComplete = false;
    bool dashComplete = false;
    bool fireComplete = false;
    SpawnManager spawnManager;

    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && !wasdComplete)
        {
            holdWASD += Time.deltaTime;
        }

        if (holdWASD >= 2 && !dashComplete)
        {
            wasdComplete = true;
            tutorialText.text = "Click (SHIFT) while moving to Dash";
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashComplete && wasdComplete)
        {
            dashComplete = true;
            tutorialText.text = "Hold (LEFT CLICK) to Fire";
            GameManager.wave = 1;
            spawnManager.StartWave();
        }

        if (Input.GetMouseButton(0) && !fireComplete && dashComplete && wasdComplete)
        {
            holdFire += Time.deltaTime;
        }

        if (holdFire >= 5 && !fireComplete && dashComplete && wasdComplete)
        {
            fireComplete = true;
            tutorialText.gameObject.SetActive(false);
        }
    }
}
