using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poptext : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
              transform.localEulerAngles = new Vector3(0, 0, 0);
     

    }
}
