using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class ShopTower : MonoBehaviour
{

    public TextMeshProUGUI popUpText;
    GameManager gameManager;
    Vector2 thisPosition;

    public Canvas shopPanel;
    public Canvas upgradePanel;
    bool isPlayerNear = false;
    bool isTowerEmpty = true;

    public int currentLevel = 0;
    public int currentTower = 0;

    [Header("Tower Prefabs")]
    public GameObject emptyTowerPrefab;

    [Header("Buttons")]
    public Button closeShopButton;
    public Button buyFreezeTowerButton;
    public Button buyRockTowerButton;
    public Button buyPoisonTowerButton;

    public Button closeUpgradeButton;
    public Button sellTowerButton;
    public Button upgradeTowerButton;


    [Header("Text")]
    public TextMeshProUGUI freezeTowerPriceText;
    public TextMeshProUGUI rockTowerPriceText;
    public TextMeshProUGUI poisonTowerPriceText;

    public TextMeshProUGUI sellPriceText;
    public TextMeshProUGUI upgradeTowerPriceText;
    public TextMeshProUGUI upgradeText;


    [Header("TowerData")]
    public TowerData freezeTowerData;
    public TowerData rockTowerData;
    public TowerData poisonTowerData;

    private void Start()
    {
        SetUI();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && isTowerEmpty)
        {
            OpenTowerShop();
        }
        else if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isTowerEmpty)
        {
            OpenTowerUpgrade();
        }
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

    void OpenTowerShop()
    {
        GameManager.isPause = true;
        Time.timeScale = 0f;

        shopPanel.gameObject.SetActive(true);
        UpdateShopUI();
    }

    void CloseTowerShop()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;

        shopPanel.gameObject.SetActive(false);
    }

    void SetUI()
    {
        gameManager = FindObjectOfType<GameManager>();
        thisPosition = transform.position;

        shopPanel.gameObject.SetActive(false);

        freezeTowerPriceText.text = freezeTowerData.price[currentLevel].ToString();
        rockTowerPriceText.text = rockTowerData.price[currentLevel].ToString();
        poisonTowerPriceText.text = poisonTowerData.price[currentLevel].ToString();

        closeShopButton.onClick.AddListener(() => CloseTowerShop());
        buyFreezeTowerButton.onClick.AddListener(() => BuyFreezeTower());
        buyRockTowerButton.onClick.AddListener(() => BuyRockTower());
        buyPoisonTowerButton.onClick.AddListener(() => BuyPoisonTower());

        closeUpgradeButton.onClick.AddListener(() => CloseTowerUpgrade());
        sellTowerButton.onClick.AddListener(() => SellTower());
        upgradeTowerButton.onClick.AddListener(() =>  UpgradeTower());
    }

    void UpdateShopUI()
    {
        if (gameManager.coins < freezeTowerData.price[currentLevel])
        {
            freezeTowerPriceText.color = Color.red;
            buyFreezeTowerButton.interactable = false;
        }
        else
        {
            freezeTowerPriceText.color = Color.black;
            buyFreezeTowerButton.interactable = true;
        }

        if (gameManager.coins < rockTowerData.price[currentLevel])
        {
            rockTowerPriceText.color = Color.red;
            buyRockTowerButton.interactable = false;
        }
        else
        {
            rockTowerPriceText.color = Color.black;
            buyRockTowerButton.interactable = true;
        }

        if (gameManager.coins < poisonTowerData.price[currentLevel])
        {
            poisonTowerPriceText.color = Color.red;
            buyPoisonTowerButton.interactable = false;
        }
        else
        {
            poisonTowerPriceText.color = Color.black;
            buyPoisonTowerButton.interactable = true;
        }

    }

    void BuyFreezeTower()
    {
        GameObject emptyTower = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Tower"))
            {
                emptyTower = child.gameObject;
            }
        }
        Destroy(emptyTower);

        Instantiate(freezeTowerData.towerPrefab, thisPosition, Quaternion.identity, transform);
        isTowerEmpty = false;

        currentTower = 1;
        UpgradeTower();

        CloseTowerShop();
    }

    void BuyRockTower()
    {
        GameObject emptyTower = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Tower"))
            {
                emptyTower = child.gameObject; 
            }
        }
        Destroy(emptyTower);

        Instantiate(rockTowerData.towerPrefab, thisPosition, Quaternion.identity, transform); 
        isTowerEmpty = false;

        currentTower = 2; 
        UpgradeTower();   

        CloseTowerShop();
    }

    void BuyPoisonTower()
    {
        GameObject emptyTower = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Tower"))
            {
                emptyTower = child.gameObject; 
            }
        }
        Destroy(emptyTower);

        Instantiate(poisonTowerData.towerPrefab, thisPosition, Quaternion.identity, transform); 
        isTowerEmpty = false;

        currentTower = 3; 
        UpgradeTower();   

        CloseTowerShop();
    }

    void UpgradeTower()
    {

        if (currentTower == 1)
        {
            gameManager.coins -= freezeTowerData.price[currentLevel];
            FreezeTowerScript towerScript = GetComponentInChildren<FreezeTowerScript>();
            towerScript.fireRate = freezeTowerData.fireRate[currentLevel];
            towerScript.damage = freezeTowerData.damage[currentLevel];
            towerScript.critRate = freezeTowerData.critRate[currentLevel];
            towerScript.critDamage = freezeTowerData.critDamage[currentLevel];
            towerScript.slowBonus = freezeTowerData.slowBonus[currentLevel];
        }
        else if (currentTower == 2)
        {
            gameManager.coins -= rockTowerData.price[currentLevel];
            TowerScript towerScript = rockTowerData.towerPrefab.GetComponent<TowerScript>();
            towerScript.fireRate = rockTowerData.fireRate[currentLevel];
            towerScript.damage = rockTowerData.damage[currentLevel];
            towerScript.critRate = rockTowerData.critRate[currentLevel];
            towerScript.critDamage = rockTowerData.critDamage[currentLevel];

        }
        else if (currentTower == 3)
        {
            gameManager.coins -= poisonTowerData.price[currentLevel];
            PoisonTower towerScript = poisonTowerData.towerPrefab.GetComponent<PoisonTower>();
            towerScript.damage = poisonTowerData.damage[currentLevel];

        }

        currentLevel++;

        CloseTowerUpgrade();
    }


    void OpenTowerUpgrade()
    {
        GameManager.isPause = true;
        Time.timeScale = 0f;

        upgradePanel.gameObject.SetActive(true);
        UpdateUpgradeUI();
    }

    void CloseTowerUpgrade()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;

        upgradePanel.gameObject.SetActive(false);
    }

    void UpdateUpgradeUI()
    {


        upgradeText.text = "Upgrade to Level " + (currentLevel + 1);

        if (currentTower == 1)
        {
            float sellPrice = freezeTowerData.price[currentLevel - 1] * 0.75f;
            sellPriceText.text = "Sell for " + sellPrice.ToString();

            if (currentLevel == 5)
            {
                upgradeText.text = "Max Level!";
                upgradeTowerPriceText.text = "Max";
                upgradeTowerPriceText.color = Color.black;
                upgradeTowerButton.interactable = false;
                return;
            }

            upgradeTowerPriceText.text = freezeTowerData.price[currentLevel].ToString();

            if (gameManager.coins < freezeTowerData.price[currentLevel])
            {
                upgradeTowerPriceText.color = Color.red;
                upgradeTowerButton.interactable = false;
            }
            else
            {
                upgradeTowerPriceText.color = Color.black;
                upgradeTowerButton.interactable = true;
            }
        }
        else if (currentTower == 2)
        {
            float sellPrice = rockTowerData.price[currentLevel - 1] * 0.75f;
            sellPriceText.text = "Sell for " + sellPrice.ToString();

            if (currentLevel == 5)
            {
                upgradeText.text = "Max Level!";
                upgradeTowerPriceText.text = "Max";
                upgradeTowerPriceText.color = Color.black;
                upgradeTowerButton.interactable = false;
                return;
            }

            upgradeTowerPriceText.text = rockTowerData.price[currentLevel].ToString();

            if (gameManager.coins < rockTowerData.price[currentLevel])
            {
                upgradeTowerPriceText.color = Color.red;
                upgradeTowerButton.interactable = false;
            }
            else
            {
                upgradeTowerPriceText.color = Color.black;
                upgradeTowerButton.interactable = true;
            }
        }
        else if (currentTower == 3)
        {
            float sellPrice = poisonTowerData.price[currentLevel - 1] * 0.75f;
            sellPriceText.text = "Sell for " + sellPrice.ToString();

            if (currentLevel == 5)
            {
                upgradeText.text = "Max Level!";
                upgradeTowerPriceText.text = "Max";
                upgradeTowerPriceText.color = Color.black;
                upgradeTowerButton.interactable = false;
                return;
            }

            upgradeTowerPriceText.text = poisonTowerData.price[currentLevel].ToString();

            if (gameManager.coins < poisonTowerData.price[currentLevel])
            {
                upgradeTowerPriceText.color = Color.red;
                upgradeTowerButton.interactable = false;
            }
            else
            {
                upgradeTowerPriceText.color = Color.black;
                upgradeTowerButton.interactable = true;
            }
        }

    }

    void SellTower()
    {
        if (currentTower == 1)
        {
            float sellPrice = freezeTowerData.price[currentLevel - 1] * 0.75f;
            gameManager.coins += sellPrice;
        }
        else if (currentTower == 2)
        {
            float sellPrice = rockTowerData.price[currentLevel - 1] * 0.75f;
            gameManager.coins += sellPrice;
        }
        else if (currentTower == 3)
        {
            float sellPrice = poisonTowerData.price[currentLevel - 1] * 0.75f;
            gameManager.coins += sellPrice;
        }

        GameObject tower = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Tower"))
            {
                tower = child.gameObject;
            }
        }
        Destroy(tower);

        currentLevel = 0;
        currentTower = 0;

        Instantiate(emptyTowerPrefab, thisPosition, Quaternion.identity, transform);
        isTowerEmpty = true;

        CloseTowerUpgrade();
    }
}
