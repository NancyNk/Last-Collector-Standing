using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            StartCoroutine(DelaySceneChange());
        }
    }

    IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);  
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
