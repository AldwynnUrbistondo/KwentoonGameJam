using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public AnimationClip smoke;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, smoke.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
