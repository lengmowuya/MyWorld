using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoltController : MonoBehaviour{
    Item BoltItem;
    Image ItemImage;
    TMP_Text ItemText;
    Image BoltImage;
    bool  isInit;
    bool isInitEnd;
    void Awake(){
        BoltImage = gameObject.GetComponent<Image>();
        ItemImage = transform.GetChild(0).GetComponent<Image>();
        ItemText = transform.GetChild(1).GetComponent<TMP_Text>();
    }
    public void UpdateBoltItem(Item newItem){
        BoltItem = newItem;
        ItemImage.sprite = BoltItem.itemImage;
        ItemText.text = BoltItem.itemName;
    }
    public void LightActive(){
        Color nowColor;
        ColorUtility.TryParseHtmlString("#000000", out nowColor);
        BoltImage.color = nowColor;
    }
    public void NormalActive(){
        Color nowColor;
        ColorUtility.TryParseHtmlString("#FFFFFF46", out nowColor);
        BoltImage.color = nowColor;
    }
}
