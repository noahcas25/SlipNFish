using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool fishPool;

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Fish"))
        fishPool.ReturnToPool(other.gameObject); 
    }  
}
