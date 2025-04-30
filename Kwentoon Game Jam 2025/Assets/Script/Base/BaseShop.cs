using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseShop : MonoBehaviour
{
    public TextMeshProUGUI popUpText;
    GameManager gameManager;

    public Canvas shopPanel;
    bool isPlayerNear = false;

    [Header("Prices")]
    public float[] damagePrice;
    public float[] fireRatePrice;
    public float[] critRatePrice;
    public float[] critDamagePrice;

    [Header("Max Level")]
    public int maxDamageLevel;
    public int maxFireRateLevel;
    public int maxCritRateLevel;
    public int maxCritDamageLevel;

    [Header("Upgrade Value")]
    public float[] damageValue;
    public float[] fireRateValue;
    public float[] critRateValue;
    public float[] critDamageValue;

    [Header("Level")]
    public int currentDamageLevel;
    public int currentFireRateLevel;
    public int currentCritRateLevel;
    public int currentCritDamageLevel;

    [Header("Buttons")]
    public Button closeShopButton;
    public Button upgradeDamageButton;
    public Button upgradeFireRateButton;
    public Button upgradeCritRateButton;
    public Button upgradeCritDamageButton;

    [Header("Text")]
    public TextMeshProUGUI damagePriceText;
    public TextMeshProUGUI fireRatePriceText;
    public TextMeshProUGUI critRatePriceText;
    public TextMeshProUGUI critDamagePriceText;

    void Start()
    {
        SetUI();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !GameManager.hasLose)
        {
            OpenUpgradeShop();
        }
    }

    void SetUI()
    {
        closeShopButton.onClick.AddListener(() => CloseUpgradeShop());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            popUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            popUpText.gameObject.SetActive(false);
        }
    }

    void OpenUpgradeShop()
    {
        GameManager.isPause = true;
        Time.timeScale = 0f;

        shopPanel.gameObject.SetActive(true);
        UpdateShopUI();
    }

    void CloseUpgradeShop()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;

        shopPanel.gameObject.SetActive(false);
    }

    void UpdateShopUI()
    {
        if (currentDamageLevel == maxDamageLevel)
        {
            damagePriceText.text = "Max";
            damagePriceText.color = Color.black;
            upgradeDamageButton.interactable = false;
            return;
        }
    }

    void UpgradeDamage()
    {
        gameManager.coins -= damagePrice[currentDamageLevel];
    }
}
