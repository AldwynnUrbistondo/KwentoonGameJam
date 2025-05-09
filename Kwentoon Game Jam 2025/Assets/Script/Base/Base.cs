using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour, IAlly
{
    [SerializeField] private float _health = 1000;
    [SerializeField] private float _maxHealth;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isDying = false;

    public GameObject damageCanvas;
    public GameObject smokePrefab;

    public float HP
    {
        get { return _health; }
        set { _health = value; }
    }

    public bool IsDying
    {
        get { return _isDying; }
        set { _isDying = value; }
    }

    public SpriteRenderer Sprite
    {
        get { return _sprite; }
        set { _sprite = value; }
    }

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        _maxHealth = _health;
    }

    void Update()
    {
        RegenHP();
    }

    public void TakeDamage(Transform bullet, float damage)
    {
        
        if (!IsDying)
        {
            HP -= damage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlaySound(SoundType.BaseHit);
            DamageText(damage);
        }
        
        if (HP <= 0 && !IsDying)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDying = true;
        GameManager.hasLose = true;

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound(SoundType.TowerDestroy);

        Instantiate(smokePrefab, transform.position, Quaternion.identity);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.black;

        Debug.Log("Damage Death: " + GameManager.damageDealth);
        Debug.Log("Waves Survived: " + (GameManager.wave - 1));
        PlayerPrefs.SetFloat("Damage Dealth", GameManager.damageDealth);
        PlayerPrefs.SetInt("Waves Survived", GameManager.wave - 1);
        PlayerPrefs.Save();

        StartCoroutine(LoseScene());
    }

    IEnumerator LoseScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("End");
    }

    void RegenHP()
    {
        if(!IsDying)
        {
            HP += Time.deltaTime * 2;
            HP = Mathf.Clamp(HP, 0, _maxHealth);
        }
    }

    #region Damage Text
    void DamageText(float damage)
    {
        GameObject canvas = Instantiate(damageCanvas, transform.position, Quaternion.identity);
        TextMeshProUGUI damageText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = "-" + damage.ToString("0");
        Destroy(canvas.gameObject, 0.6f);

        StartCoroutine(AnimateText(canvas.transform));
    }

    IEnumerator AnimateText(Transform canvasTransform)
    {
        Vector3 startPos = canvasTransform.position;
        Vector3 endPos = startPos + new Vector3(0, 0.5f, 0);
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            canvasTransform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }


    }

    #endregion
}
