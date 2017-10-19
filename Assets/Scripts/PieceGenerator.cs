using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class just basically pulls a prefab from a prefab set and spawns it in at its position.  
//Used for one-off random set pieces within a "room", or to add small variety to each room
//This shoiuld be "chainable" (if a prefab in the set also has this script on it)
//So you could spawn in a bookshelf randomly, then spawn in a bunch of random books as well
public class PieceGenerator : MonoBehaviour {

    public PrefabSet PiecePrefabSet;

    private GameObject _generatedObject;

    void OnEnable()
    {
        GameObject prefabToUse = PiecePrefabSet.GetRandomSetPrefab();
        _generatedObject = ObjectPoolManager.GetObject(prefabToUse);
        _generatedObject.transform.parent = this.transform;
        _generatedObject.transform.localPosition = Vector3.zero;
        _generatedObject.transform.localRotation = Quaternion.identity;
    }

    void OnDisable()
    {
        if (_generatedObject != null)
        {
            //have to return with a delay because Unity won't let you change the parent on the same frame that you disable an object
            ObjectPoolManager.ReturnObjectDelayed(_generatedObject, .01f); 
        }
    }


}
