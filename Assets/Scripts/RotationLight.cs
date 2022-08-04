using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLight : MonoBehaviour
{
    private float rotateSpeed = 3;
    void Update(){
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime,Space.Self);
    }
}
