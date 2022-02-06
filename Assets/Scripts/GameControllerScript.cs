using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject fish, centerPoint;
    [SerializeField]
    private GameObjectPool fishPool;

    private bool shouldSpawn = true;

    void Start() {
        fishPool.AddToPool(3);
    }

    // Update is called once per frame
    void Update()
    {
      if(shouldSpawn)
        SpawnFish();
    }

    private void SpawnFish() {
        GameObject newFish = Instantiate(fish);
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

    IEnumerator SpawnTimer() {
        shouldSpawn = false;
        yield return new WaitForSeconds(2f);
        shouldSpawn = true;
    }
}
