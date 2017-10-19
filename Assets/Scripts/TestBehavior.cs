using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavior : MonoBehaviour {

    public static bool white = true;

    MeshRenderer renderer;

	// Use this for initialization
	void Awake () {
        renderer = GetComponent<MeshRenderer>();
	}
	
	void OnEnable () {
        if (white)
        {
            renderer.material.color = Color.white;
            white = false;
        }
        else
        {
            renderer.material.color = Color.black;
            white = true;
        }
	}
}
