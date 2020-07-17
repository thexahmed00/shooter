using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
  public float SpinSpeed;
   public float  motion;
    public enum motionDirections {Spin,Horizontal,Vertical };
    public motionDirections motionStates = motionDirections.Horizontal;
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        switch (motionStates)
        {
            case motionDirections.Spin:
                transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime);
                break;
            case motionDirections.Horizontal:
                transform.Translate(Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad) * motion*Time.deltaTime);
                break;
            case motionDirections.Vertical:
                transform.Translate(Vector3.up * Mathf.Cos(Time.timeSinceLevelLoad) * motion * Time.deltaTime);
                break;
        }
       
    }
}
