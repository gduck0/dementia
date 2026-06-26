using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EventManager : MonoBehaviour
{

    public enum EventType { Plate, Gift, Phone, Clue1, Clue2, Clue3, Clue4, Mirror, FamilyPhoto } //오브젝트 종류
    public EventType eventType;
    public static event Action<EventType> OnEvent;
    private XRGrabInteractable grab;

    // gaze 관련
    public float gazeTime = 1f;
    private float gazeTimer = 0f;
    private bool isGazed = false;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (grab != null)
            grab.selectEntered.AddListener(OnGrab);

        GazeRazcaster.OnGazeTarget += HandleGaze;
    }

    private void OnDisable()
    {
        if (grab != null)
            grab.selectEntered.RemoveListener(OnGrab);

        GazeRazcaster.OnGazeTarget -= HandleGaze;
    }

    private void OnGrab(SelectEnterEventArgs args) //오브젝트 종류 전달
    {
        OnEvent?.Invoke(eventType);
    }
    private void HandleGaze(EventManager target)
    {
        if (target == this)
            isGazed = true;
        else
        {
            isGazed = false;
            gazeTimer = 0f;
        }
    }
    private void Update()
    {
        if (eventType != EventType.Mirror && eventType != EventType.FamilyPhoto)
            return;

        if (isGazed)
        {
            gazeTimer += Time.deltaTime;

            if (gazeTimer >= gazeTime)
            {
                Debug.Log("거울 구별 성공");
                OnEvent?.Invoke(eventType);
                gazeTimer = 0f; // 반복 실행 방지
            }
        }
    }

}

