using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler {

    public void OnPointerEnter(PointerEventData ev) {
        Debug.Log("DDDD");
    }

    public void OnPointerExit(PointerEventData ev) {
        Debug.Log("AAAA");
    }

}
