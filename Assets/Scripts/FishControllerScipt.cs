using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControllerScipt : MonoBehaviour
{
    private float speed = 3;
    private int pointValue = 50;

    void Start() {

        if(Random.Range(0,20)==0) {
            changeScale(1f);
            setPointValue(200);
            setSpeed(5);
        }
        else{
            changeScale(Random.Range(0.3f, 0.6f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    } 

    public void changeScale(float value) => transform.localScale = new Vector3(value, value, value);

    public void setPointValue(int newPointValue) => this.pointValue = newPointValue;

    public void setSpeed(float value) => this.speed = value;

}
