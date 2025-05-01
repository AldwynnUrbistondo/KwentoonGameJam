using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class delayDisable : MonoBehaviour
{
    public Button button;
    



    public void delayActivate()
    {
        Invoke("disableButton", 0.1f);
    }

    public void disableButton()
    {
        button.gameObject.SetActive(false);
    }
}
