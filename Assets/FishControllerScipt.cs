using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControllerScipt : MonoBehaviour
{
    // [SerializeField]
    // Transform centerPoint;



    // Update is called once per frame
    void Update()
    {
        transform.position = transform.forward * Time.deltaTime * 1;
    } 
}
