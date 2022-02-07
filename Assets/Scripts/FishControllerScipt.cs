using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControllerScipt : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private int pointValue = 50;
    [SerializeField]
    private int timeValue = 2;

    void Start() {

        if(Random.Range(0,13)==0) {
            changeScale(1f);
            setPointValue(200);
            setTimeValue(3);
            setSpeed(7);
        }
        else{
            changeScale(Random.Range(0.4f, 0.8f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    } 

    public void changeScale(float value) => transform.localScale = new Vector3(value, value, value);

    public int getPointValue() => this.pointValue;

    public void setPointValue(int newPointValue) => this.pointValue = newPointValue;

    public int getTimeValue() => this.timeValue;

    public void setTimeValue(int newTimeValue) => this.timeValue = newTimeValue;

    public void setSpeed(float value) => this.speed = value;

}
