using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public Vector3 a;
    public Vector3 b;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestDotProduct();
        }
	}

    void TestDotProduct()
    {
        Vector3 cross = Vector3.Cross(a, b);
        float angle = Vector3.Angle(a, b);
        if (cross.y < 0) angle *= -1;

        print(angle);
    }

}
