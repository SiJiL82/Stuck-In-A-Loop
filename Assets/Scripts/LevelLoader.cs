using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName, float delayTime= 0)
    {
        StartCoroutine(WaitThenLoadScene(sceneName, delayTime));
    }

    IEnumerator WaitThenLoadScene(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(sceneName);
    }
}
