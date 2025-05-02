using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseShop : MonoBehaviour
{
    public TextMeshProUGUI popUpText;
    GameManager gameManager;
    AudioManager audioManager;

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
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

        closeShopButton.onClick.AddListener(() => CloseUpgradeShop());

        upgradeDamageButton.onClick.AddListener(() => UpgradeDamage());
        upgradeFireRateButton.onClick.AddListener(() => UpgradeFireRate());
        upgradeCritRateButton.onClick.AddListener(() => UpgradeCritRate());
        upgradeCritDamageButton.onClick.AddListener(() => UpgradeCritDamage());
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
        audioManager.PlaySound(SoundType.ButtonClick);

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
        }
        else
        {
            damagePriceText.text = damagePrice[currentDamageLevel].ToString();

            if (gameManager.coins < damagePrice[currentDamageLevel])
            {
                damagePriceText.color = Color.red;
                upgradeDamageButton.interactable = false;
            }
            else
            {
                damagePriceText.color = Color.black;
                upgradeDamageButton.interactable = true;
            }
        }

        if (currentFireRateLevel == maxFireRateLevel)
        {
            fireRatePriceText.text = "Max";
            fireRatePriceText.color = Color.black;
            upgradeFireRateButton.interactable = false;
        }
        else
        {
            fireRatePriceText.text = fireRatePrice[currentFireRateLevel].ToString();

            if (gameManager.coins < fireRatePrice[currentFireRateLevel])
            {
                fireRatePriceText.color = Color.red;
                upgradeFireRateButton.interactable = false;
            }
            else
            {
                fireRatePriceText.color = Color.black;
                upgradeFireRateButton.interactable = true;
            }
        }

        if (currentCritRateLevel == maxCritRateLevel)
        {
            critRatePriceText.text = "Max";
            critRatePriceText.color = Color.black;
            upgradeCritRateButton.interactable = false;
        }
        else
        {
            critRatePriceText.text = critRatePrice[currentCritRateLevel].ToString();

            if (gameManager.coins < critRatePrice[currentCritRateLevel])
            {
                critRatePriceText.color = Color.red;
                upgradeCritRateButton.interactable = false;
            }
            else
            {
                critRatePriceText.color = Color.black;
                upgradeCritRateButton.interactable = true;
            }
        }

        if (currentCritDamageLevel == maxCritDamageLevel)
        {
            critDamagePriceText.text = "Max";
            critDamagePriceText.color = Color.black;
            upgradeCritDamageButton.interactable = false;
        }
        else
        {
            critDamagePriceText.text = critDamagePrice[currentCritDamageLevel].ToString();

            if (gameManager.coins < critDamagePrice[currentCritDamageLevel])
            {
                critDamagePriceText.color = Color.red;
                upgradeCritDamageButton.interactable = false;
            }
            else
            {
                critDamagePriceText.color = Color.black;
                upgradeCritDamageButton.interactable = true;
            }
        }
    }

    void UpgradeDamage()
    {
        audioManager.PlaySound(SoundType.ButtonBuy);
        gameManager.coins -= damagePrice[currentDamageLevel];

        PlayerAttack playerStats = FindObjectOfType<PlayerAttack>();
        playerStats.damage = damageValue[currentDamageLevel];

        currentDamageLevel++;

        UpdateShopUI();
    }

    void UpgradeFireRate()
    {
        audioManager.PlaySound(SoundType.ButtonBuy);
        gameManager.coins -= fireRatePrice[currentFireRateLevel];

        PlayerAttack playerStats = FindObjectOfType<PlayerAttack>();
        playerStats.fireRate = fireRateValue[currentFireRateLevel];

        currentFireRateLevel++;

        UpdateShopUI();
    }

    void UpgradeCritRate()
    {
        audioManager.PlaySound(SoundType.ButtonBuy);
        gameManager.coins -= critRatePrice[currentCritRateLevel];

        PlayerAttack playerStats = FindObjectOfType<PlayerAttack>();
        playerStats.critRate = critRateValue[currentCritRateLevel];

        currentCritRateLevel++;

        UpdateShopUI();
    }

    void UpgradeCritDamage()
    {
        audioManager.PlaySound(SoundType.ButtonBuy);
        gameManager.coins -= critDamagePrice[currentCritDamageLevel];

        PlayerAttack playerStats = FindObjectOfType<PlayerAttack>();
        playerStats.critDamage = critDamageValue[currentCritDamageLevel];

        currentCritDamageLevel++;

        UpdateShopUI();
    }

}
