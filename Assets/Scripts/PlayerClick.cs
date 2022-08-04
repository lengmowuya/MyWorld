using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PlayerClick : MonoBehaviour{
    private Ray ray;    // 发出的射线
    private RaycastHit hitInfo;     // 射线信息
    private LayerMask mask;     //  碰撞层
    public GameObject GrassCube;
    public GameObject MainMenu;
    public GameObject CubeFather;
    void Start(){
        Cursor.visible = true;
        Cursor.lockState = false?CursorLockMode.Confined : CursorLockMode.Locked;
        // mask = 1 << 8;      //  只开启第八层
        // mask = ~(1 << 8);    // 除了第八层都碰撞
        mask = 1 << LayerMask.NameToLayer("Cube");   // 同上，Cube是第八层的名字
    }

    void Update(){

        if(Input.GetMouseButtonDown(0)){
            DestroyCube();
        }
        if(Input.GetMouseButtonDown(1)){
            CreateCube();
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            OpenMenu();
        }
        Cursor.visible = true;
        // Cursor.lockState = false ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    void OpenMenu(){
        if(MainMenu.activeSelf == true){
            MainMenu.SetActive(false);
            Cursor.lockState = false?CursorLockMode.Confined : CursorLockMode.Locked;
        }else{
            MainMenu.SetActive(true);
            Cursor.lockState = true?CursorLockMode.Confined : CursorLockMode.Locked;
        }

    }

    private void DestroyCube(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // 从相机位置朝当前鼠标位置发射射线
        if(Physics.Raycast(ray,out hitInfo,1000,mask)){     // 射线长度1000，碰撞mask层
            Destroy(hitInfo.collider.gameObject);
        }
    }
    private void CreateCube(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // 从相机位置朝当前鼠标位置发射射线
        if(Physics.Raycast(ray,out hitInfo,1000,mask)){     // 射线长度1000，碰撞mask层
            // UnityEngine.Debug.Log("碰撞点"+ hitInfo.point +"碰到了 物体" + hitInfo.collider.gameObject.transform.position);
            // 在哪个方向，碰撞点偏离物体原点 ≥ 0.5的距离（物体的半径）
            // 然后记作偏移 (0,0.5,0)
            // 创建一个新物体，在碰撞物体的坐标基础上移动偏移量
            GameObject oldGameObject = hitInfo.collider.gameObject;
            Vector3 newMove = new Vector3();
            Vector3 point = hitInfo.point;
            Vector3 oldPoint = hitInfo.collider.gameObject.transform.position;
            if(Mathf.Abs(point.x - oldPoint.x) >= 0.5f){
                if((point.x - oldPoint.x)>= 0){
                    newMove.x += 1f;
                }else{
                    newMove.x -= 1f;
                }
            }
            if(Mathf.Abs(point.y - oldPoint.y) >= 0.5f){
                if((point.y - oldPoint.y)>= 0){
                    newMove.y += 1f;
                }else{
                    newMove.y -= 1f;
                }
            }
            if(Mathf.Abs(point.z - oldPoint.z) >= 0.5f){
                if((point.z - oldPoint.z)>= 0){
                    newMove.z += 1f;
                }else{
                    newMove.z -= 1f;
                }
            }
            newMove += oldPoint;
            // UnityEngine.Debug.Log(newMove);
            // UnityEngine.Debug.Log(BeltController.Ins.GetCube());
            GameObject activeCube = BeltController.Ins.GetCube();
            GameObject newCube = Instantiate(activeCube,newMove,oldGameObject.transform.rotation);
            newCube.transform.SetParent(CubeFather.transform);
        }
    }
}