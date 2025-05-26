using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M1_SlideControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object1; // 기본 오브젝트
    public GameObject object2; // 슬라이드 중 오브젝트

    public GroundChecker groundChecker;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (groundChecker == null || !groundChecker.isGrounded) return;

        object2.transform.position = object1.transform.position;
        object2.transform.rotation = object1.transform.rotation;

        object1.SetActive(false);
        object2.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (groundChecker == null || groundChecker.isGrounded) return;

        object1.transform.position = object2.transform.position;
        object1.transform.rotation = object2.transform.rotation;

        object1.SetActive(true);
        object2.SetActive(false);
    }
}
