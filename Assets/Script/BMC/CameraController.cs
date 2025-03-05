using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeDuration = 0.3f; //
                                       // ��鸮�� ���� �ð�
    public float shakeMagnitude = 0.2f; // ��鸲 ����

    Vector3 originalPosition;
    [field: SerializeField] public Vector2 ScreenArea { get; private set; } // ȭ�� ũ��
    void Start()
    {
        originalPosition = transform.position;

        // ī�޶��� ȭ�� ��踦 ���� ��ǥ�� ��ȯ
        ScreenArea = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
    }

    // ī�޶� ȭ�� ����
    public IEnumerator ShakeCamera()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            Camera.main.transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPosition; // ���� ��ġ�� ����
    }
}