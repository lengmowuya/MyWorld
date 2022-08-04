using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    private Item[] itemList;
    public GameObject[] childBolt;
    int BeltLength = 9;
    int NowActive = 1;
    private static BeltController _Ins;
    public static BeltController Ins {get {return _Ins;}}
    void Awake(){
        itemList = CubeAssetsManager.Ins.ItemList;
    }
    void Start()
    {
        if(!_Ins){
            _Ins = this;
        }
        updateBoltUI();
    }

    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            changeActive(false);
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0){
            changeActive(true);
        }
    }
    void changeActive(bool add){
        if(add){
            NowActive ++;
        }else{
            NowActive --;
        }
        if(NowActive>BeltLength){
            NowActive = 1;
        }
        if(NowActive < 1){
            NowActive = BeltLength;
        }
        // Debug.Log(NowActive);
        updateActiveBolt(NowActive);
    }
    void updateBoltUI(){
        for(int i = 0; i < itemList.Length; i++){
            BoltController Bolt = transform.GetChild(i).GetComponent<BoltController>();
            Bolt.UpdateBoltItem(itemList[i]);
        }
        updateActiveBolt(NowActive);
    }
    void allBoltNormalBolt(){
        int index = 0;
        foreach(Transform child in transform){
            BoltController Bolt = transform.GetChild(index).GetComponent<BoltController>();
            Bolt.NormalActive();
            index++;
        }
    }
    void updateActiveBolt(int activeIndex){
        allBoltNormalBolt();
        BoltController Bolt = transform.GetChild(activeIndex-1).GetComponent<BoltController>();
        Bolt.LightActive();
    }
    public GameObject GetCube(){
        // Debug.Log(itemList.Length);
        return itemList[NowActive-1].Prefab;
    }
}
