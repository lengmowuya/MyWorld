using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAssetsManager : MonoBehaviour{
    public Item[] ItemList;
    private static CubeAssetsManager _Ins;
    public static CubeAssetsManager Ins {get {return _Ins;}}
    void Awake()
    {
        // Application.targetFrameRate = 240;
        if(!_Ins){
            _Ins = this;
        }
    }
    public Item getCubeType(string name){
        Item returnType = null;
        for(int i = 0; i < ItemList.Length;i++){
            if(ItemList[i].itemName == name){
                returnType = ItemList[i];
            }
        }
        return returnType;
    }
}