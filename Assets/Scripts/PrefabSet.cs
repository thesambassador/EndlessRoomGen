using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabSetEntry
{
    public GameObject Prefab;
    public int Probability = 1;

}

//Defines a set of potential prefabs to use for level generation
[CreateAssetMenu(fileName = "PrefabSet")]
public class PrefabSet : ScriptableObject {

    public string Name;
    public PrefabSetEntry[] PrefabArray;

    void Awake()
    {
        
    }

    public GameObject GetRandomSetPrefab()
    {
        List<GameObject> possibilities = new List<GameObject>();
        for (int i = 0; i < PrefabArray.Length; i++)
        {
            for (int j = 0; j < PrefabArray[i].Probability; j++)
            {
                possibilities.Add(PrefabArray[i].Prefab);
            }
        }

        return possibilities[Random.Range(0, possibilities.Count)];
    }

    public GameObject GetRandomValidSetPrefab(System.Predicate<GameObject> validFunction)
    {
        List<GameObject> possibilities = new List<GameObject>();
        for (int i = 0; i < PrefabArray.Length; i++)
        {
            if (validFunction.Invoke(PrefabArray[i].Prefab))
            {
                for (int j = 0; j < PrefabArray[i].Probability; j++)
                {
                    possibilities.Add(PrefabArray[i].Prefab);
                }
            }
        }

        if (possibilities.Count > 0)
        {
            return possibilities[Random.Range(0, possibilities.Count)];
        }
        else
        {
            Debug.Log("No valid prefabs for that predicate");
            return null;
        }
    }

    public GameObject[] GetShuffledPrefabSet()
    {
        GameObject[] result = new GameObject[PrefabArray.Length];
        for (int i = 0; i < PrefabArray.Length; i++)
        {
            result[i] = PrefabArray[i].Prefab;
        }
        
        result.Shuffle<GameObject>();
        return result;
    }



	
}
