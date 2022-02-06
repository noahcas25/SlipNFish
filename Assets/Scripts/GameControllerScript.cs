using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject fish, centerPoint, timer, UIscore, floatingText;

    [SerializeField]
    private GameObjectPool fishPool, textPool;

    [SerializeField]
    private AudioClip penguinCall, whistle;

    private AudioSource audio;
    private bool shouldSpawn = true;
    private bool gameOver;
    private int score;
    private int count;

    void Start() {
        fishPool.AddToPool(15);
        textPool.AddToPool(5);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if(shouldSpawn && !gameOver)
        SpawnFish();
    }

    private void SpawnFish() {
        GameObject newFish = fishPool.Get();
        newFish.SetActive(true);

        switch(Random.Range(0,4)) {
            case 0: 
                newFish.transform.position = new Vector3(Random.Range(-12, 13), 1, 2);
            break;
            case 1:
                newFish.transform.position = new Vector3(12, 1, Random.Range(-14, 3));
            break;
            case 2:
                newFish.transform.position = new Vector3(Random.Range(-12, 13), 1, -14);
            break;
            case 3:
                newFish.transform.position = new Vector3(-12, 1, Random.Range(-14, 3));
            break;
        }

        newFish.transform.LookAt(centerPoint.transform);
        newFish.transform.Rotate(0, Random.Range(-25, 26), 0);
        StartCoroutine(SpawnTimer());
    }

    public void FishCaught(GameObject fish) {
        if(!gameOver) {
            timer.GetComponent<TimerScript>().AddTime(fish.GetComponent<FishControllerScipt>().getTimeValue());
            score += fish.GetComponent<FishControllerScipt>().getPointValue();
            UIscore.GetComponent<TMPro.TextMeshProUGUI>().text = "" + score;
            if(fish.GetComponent<FishControllerScipt>().getPointValue() > 100) 
                audio.PlayOneShot(penguinCall);

            count++;
            StartCoroutine(PoolDelay(fish));
        }
    }

    public void GameOver() {
        gameOver = true;
        audio.PlayOneShot(whistle);
        timer.transform.GetChild(0).gameObject.SetActive(true);

        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay() {
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
    }

    IEnumerator PoolDelay(GameObject fish) {
        GameObject newText = textPool.Get();
        newText.SetActive(true);

        newText.transform.position = fish.transform.position;
        newText.GetComponent<TextMesh>().text = fish.GetComponent<FishControllerScipt>().getPointValue() + "";
        fish.SetActive(false);

        yield return new WaitForSeconds(1f);
        fishPool.ReturnToPool(fish);
        textPool.ReturnToPool(newText);
    }

    IEnumerator SpawnTimer() {
        shouldSpawn = false;
        yield return new WaitForSeconds(1.5f);
        shouldSpawn = true;
    }
}
