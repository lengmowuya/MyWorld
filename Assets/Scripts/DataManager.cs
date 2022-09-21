using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Text;
using System;
public class DataManager : MonoBehaviour{
    public GameObject CubeFather;
    private List<CubeData> WorldCube;
    private GameObject[] NowCubeList;
    void Start(){
        WorldCube = new List<CubeData>();
        GetCubeList();
    }
    public void SaveCube(){
        // Debug.Log(Application.persistentDataPath);
        WorldCube = new List<CubeData>();
        GetCubeList();
        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData")){
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/WorldCube.json");

        String hingeString = JsonConvert.SerializeObject(WorldCube);
        Debug.Log(WorldCube.Count);
        formatter.Serialize(file,hingeString);
        file.Close();
    }
    public void GetCubeList(){
        foreach(Transform child in CubeFather.transform){
            CubeData newData = new CubeData();
            newData.x = child.position.x;
            newData.y = child.position.y;
            newData.z = child.position.z;
            newData.cubeName = child.GetComponent<CubeSelf>().cubeName;
            WorldCube.Add(newData);
        }
    }
    List<CubeData> ReadWorld;
    public void LoadCube(){
        ReadWorld  = new List<CubeData>();
        BinaryFormatter bf = new BinaryFormatter();
        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData")) return;

        FileStream file  = File.Open(Application.persistentDataPath + "/game_SaveData/WorldCube.json",FileMode.Open);

        string readJson = (string)bf.Deserialize(file);
        ReadWorld = JsonConvert.DeserializeObject<List<CubeData>>(readJson);
        Debug.Log(ReadWorld.Count);
        file.Close();
        DestoryChildren();
        for(int i = 0; i< ReadWorld.Count;i++){
            CubeData cube = ReadWorld[i];
            Vector3 cubePosition = new Vector3(cube.x,cube.y,cube.z);
            // 找不到方块就跳出
            if(CubeAssetsManager.Ins.getCubeType(cube.cubeName) == null) {
                Debug.Log(cube.cubeName);
                continue;
            };
            GameObject cubeType = CubeAssetsManager.Ins.getCubeType(cube.cubeName).Prefab;
            GameObject newCube = Instantiate(cubeType,cubePosition,Quaternion.Euler(0f, 0f, 0f));
            newCube.transform.SetParent(CubeFather.transform);
        }

    }

    public void DestoryChildren(){
        foreach(Transform child in CubeFather.transform){
            Destroy(child.gameObject);
        }
    }
}
