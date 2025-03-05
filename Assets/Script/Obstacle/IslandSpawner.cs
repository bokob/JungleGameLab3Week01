using TMPro.Examples;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    [Header("��")]
    public int spawnIslandCount = 2;
    public float minDistance = 2f; // �� �� �ּ� �Ÿ�
    public Vector2 screenArea;
    public BoxCollider2D areaCollider;
    [SerializeField] GameObject[] IslandPrefabArray = new GameObject[5];

    [Header("����̵�")]
    public int spawnTornadoCount = 2;

    void Start()
    {
        // ī�޶��� ȭ�� ��踦 ���� ��ǥ�� ��ȯ�Ͽ� ���� ũ��� ����
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        screenArea = cameraController.ScreenArea;
        areaCollider.size = screenArea;             // ���� ũ��� ����

        SpawnIsland();
    }

    void SpawnIsland()
    {
        for(int i=0; i<spawnIslandCount; i++)
        {
            Vector2 spawnPosition;
            int attempts = 0;
            do
            {
                float x = Random.Range(-areaCollider.size.x, areaCollider.size.x);
                float y = Random.Range(-areaCollider.size.y, areaCollider.size.y);

                // ���� �������� ��ǥ ����
                spawnPosition = new Vector2(x, y);
                attempts++;
            }
            while (Physics2D.OverlapCircle(spawnPosition, minDistance) != null && attempts < 100);

            if(attempts < 100)
            {
                int randomIdx = Random.Range(0, 5);
                Instantiate(IslandPrefabArray[randomIdx], spawnPosition, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmos()
    {
        Color color = new Color(1, 0, 0, 0.25f);
        Gizmos.color = color;

        Vector2 area = areaCollider.size;
        Gizmos.DrawCube(transform.position, area);
    }
}
