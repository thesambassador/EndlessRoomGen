using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsFromSetOnDelay : MonoBehaviour {

    public PrefabSet setToUse;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnDelay(.1f));
	}
	
	// Update is called once per frame
	void Update () {
		


	}

    IEnumerator SpawnDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            GameObject spawnObject = Instantiate(setToUse.GetRandomSetPrefab(), transform.position, Quaternion.identity);
            spawnObject.GetComponent<Rigidbody>().AddForce(0, 1000, 0);
            
        }

    }
}
