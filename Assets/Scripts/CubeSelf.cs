using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelf : MonoBehaviour
{
    public Item cubeType;
    public string cubeName;
    void Start(){
        cubeName = cubeType.itemName;
    }
}
