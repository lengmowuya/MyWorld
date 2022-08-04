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
        // Debug.Log(WorldCube.Count);
    }
    List<CubeData> ReadWorld;
    public void LoadCube(){
        ReadWorld  = new List<CubeData>();
        BinaryFormatter bf = new BinaryFormatter();
        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData")) return;

        FileStream file  = File.Open(Application.persistentDataPath + "/game_SaveData/WorldCube.json",FileMode.Open);

        string readJson = (string)bf.Deserialize(file);
        ReadWorld = JsonConvert.DeserializeObject<List<CubeData>>(readJson);
        // Debug.Log(ReadWorld);
        Debug.Log(ReadWorld.Count);
        // Debug.Log(ReadWorld[0].cubeName);
        file.Close();

        DestoryChildren();
        CubeAssetsManager Ins = CubeAssetsManager.Ins;
        for(int i = 0; i< ReadWorld.Count;i++){
            CubeData cube = ReadWorld[i];
            Vector3 cubePosition = new Vector3(cube.x,cube.y,cube.z);
            Debug.Log(Ins.getCubeType);
            GameObject cubeType = Ins.getCubeType(cube.cubeName).Prefab;
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
