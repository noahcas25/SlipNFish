using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject timerFill, timerText, gameController;

    private int startDuration, remaining;
    
    // Start is called before the first frame update
    void Start() {
       startDuration = 30;
       StartCoroutine(StartDelay(0));  
    }

    private void startTimer() {
        remaining = startDuration;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer() {
        while(remaining >= 0) {
            UIUpdate();
            
            if(remaining==0)
                break;
        
            remaining--;
            yield return new WaitForSeconds(1f);
        }
        gameController.GetComponent<GameControllerScript>().GameOver();
    }

    public void AddTime(int extraTime) {
        this.remaining += extraTime;
        UIUpdate();
    } 

    private void UIUpdate() {
        timerText.GetComponent<TMPro.TextMeshProUGUI>().text = "" + remaining;
        timerFill.GetComponent<Image>().fillAmount = Mathf.InverseLerp(0, startDuration, remaining);
    }

    IEnumerator StartDelay(float time) {
        yield return new WaitForSeconds(time);
        startTimer();
    }
}
