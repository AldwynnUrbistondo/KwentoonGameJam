using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    public string SceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void delaylaodScene()
    {
        Invoke("SceneLoad", 1f);
    }

    public void SceneLoad()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }

    public void PauseGame()
    {
        GameManager.isPause = true;
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        GameManager.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
