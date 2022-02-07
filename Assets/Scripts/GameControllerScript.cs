using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player, fish, centerPoint, timer, UIscoreInGame, floatingText, UIFishCount, UIscoreEndGame, UIHighScore;

    [SerializeField]
    private GameObjectPool fishPool, textPool;

    [SerializeField]
    private AudioClip penguinCall, whistle;

    private AudioSource audio;
    private bool shouldSpawn = true;
    private bool gameOver;
    private int score, highScore = 0;
    private int count;

    void Start() {
        fishPool.AddToPool(15);
        textPool.AddToPool(5);
        audio = GetComponent<AudioSource>();

        audio.PlayOneShot(whistle);
        timer.transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnEnable() {
        if(PlayerPrefs.HasKey("Highscore"))
            highScore = PlayerPrefs.GetInt("Highscore");
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
            UIscoreInGame.GetComponent<TMPro.TextMeshProUGUI>().text = "" + score;
            if(fish.GetComponent<FishControllerScipt>().getPointValue() > 100) 
                audio.PlayOneShot(penguinCall);

            count++;
            StartCoroutine(PoolDelay(fish));
        }
    }

    public void GameOver() {
        gameOver = true;
        player.GetComponent<PlayerControllerScript>().setGameOver(gameOver);
        timer.transform.GetChild(1).gameObject.SetActive(true);
        audio.PlayOneShot(whistle);

        if(score > highScore) {
            PlayerPrefs.SetInt("Highscore", score);
            highScore = score;
        }

        UIFishCount.GetComponent<TMPro.TextMeshProUGUI>().text = count + "";
        UIscoreEndGame.GetComponent<TMPro.TextMeshProUGUI>().text = score + "";
        UIHighScore.GetComponent<TMPro.TextMeshProUGUI>().text = highScore + "";

        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay() {
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
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

    public void Restart() { 
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void Quit() => Application.Quit();
}
