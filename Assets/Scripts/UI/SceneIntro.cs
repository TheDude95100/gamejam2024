using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIntro : MonoBehaviour
{
    float actualTime;
    

    private void Update()
    {
        actualTime += Time.deltaTime;
        //Debug.Log(actualTime);
        if (actualTime >= 6)
        {
            Debug.Log("test");
            SceneManager.LoadScene("MenuPricipal");
        }
        
    }
}
