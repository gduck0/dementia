using UnityEngine;
using System;

public class GazeRazcaster : MonoBehaviour
{
    public static event Action<EventManager> OnGazeTarget;

    private Transform cam;
    private float distance = 20f;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            EventManager em = hit.collider.GetComponent<EventManager>();

            if (em != null)
            {
                OnGazeTarget?.Invoke(em);
                return;
            }
        }

        // 아무것도 안 보면 null 전달
        OnGazeTarget?.Invoke(null);
    }
}
