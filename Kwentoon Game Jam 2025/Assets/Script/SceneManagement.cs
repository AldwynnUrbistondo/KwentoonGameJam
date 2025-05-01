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
        SceneManager.LoadScene(SceneName);
    }

}
