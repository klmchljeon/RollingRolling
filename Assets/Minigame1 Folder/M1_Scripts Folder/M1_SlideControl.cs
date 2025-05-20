using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M1_SlideControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object1; //기본상태
    public GameObject object2; // 눌렀을 때

    public void OnPointerDown(PointerEventData eventData)
    {
        object1.SetActive(false);
        object2.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        object1.SetActive(true);
        object2.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
