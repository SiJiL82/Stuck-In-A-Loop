using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_StartMenu : MonoBehaviour
{
    //Config
    [SerializeField] GameObject playButton = null;
    [SerializeField] GameObject titleTextModel_prefab = null;
    [SerializeField] float titleTextSpawnRate = 3f;
    [SerializeField] float spawnForce = 100f;

    //Variables
    private bool playButtonPushed;

    //References
    
    void Start()
    {
        //playButton.GetComponent<PlayButton>().hasBeenClicked
        GetPlayButtonPushed();
        StartCoroutine(SpawnTitleText());
    }

    IEnumerator SpawnTitleText()
    {
        while(!playButtonPushed)
        {
            Debug.Log(playButtonPushed);
            GameObject titleText = Instantiate(titleTextModel_prefab, transform.position, titleTextModel_prefab.transform.rotation, transform);
            //GameObject titleText = Instantiate(titleTextModel_prefab, transform.position, transform.rotation, transform);
            Vector3 force = new Vector3(Random.Range(-spawnForce, spawnForce), 0f, 0f);
            titleText.GetComponent<Rigidbody>().AddForce(force);
            Destroy(titleText, 30f);
            GetPlayButtonPushed();

            yield return new WaitForSeconds(titleTextSpawnRate);
        }
    }

    private void GetPlayButtonPushed()
    {
        playButtonPushed = playButton.GetComponent<PlayButton>().hasBeenClicked;
    }

    
}
