using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] public Vector2 ScreenArea { get; private set; } // ȭ�� ũ��
    void Start()
    {
        // ī�޶��� ȭ�� ��踦 ���� ��ǥ�� ��ȯ
        ScreenArea = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
    }
}