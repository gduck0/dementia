using UnityEngine;

public class ExampleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime * 60f, 0f);
    }
}
