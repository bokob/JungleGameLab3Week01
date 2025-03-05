using UnityEngine;
using UnityEngine.LightTransport;
using static UnityEngine.GraphicsBuffer;

public class Spear : MonoBehaviour
{
    public float speed = 6f; // �̵� �ӵ�
    public float returnSpeed = 3f;
    public Vector3 targetPosition; // Ŭ���� ��ġ
    public Vector3 startPosition; // ���� ��
    public float acceleration = 2f; // ���ӵ�
    public GameObject tail;    // �� ���� �κ�

    private float currentSpeed = 0f; // ���� �ӵ�

    private Vector3 originalScale; // ���� ũ��
    private bool isMoving = false;
    private bool isReturn = false;
    private float journeyLength;

    float distanceCovered = 0f;
    float fractionOfJourney = 0f;

    GameObject enemy;
    GameObject playerObj;
   
    Transform[] points = new Transform[2];

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0f); // 3D ��ǥ�� 2D�� ����
        journeyLength = Vector3.Distance(startPosition, targetPosition);
        SetTail();
        isMoving = true;
    }

    // ���� ����
    void SetTail()
    {
        points[0] = playerObj.transform;
        points[1] = gameObject.transform;
        tail.GetComponent<LineController>().SetUpLine(points);
    }

    void Update()
    {

        if (isMoving)
        {
            distanceCovered += Time.deltaTime * speed; // �̵� �Ÿ�
            transform.up = (targetPosition - startPosition).normalized; // �ۻ� ��ü ����

            // �̵�
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed*Time.deltaTime);

            // ������ �����ϸ� ����
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                    Bbb10311031_SoundManager.instance.PlaySFX("Clash");
                }
                else
                {
                    Bbb10311031_SoundManager.instance.PlaySFX("SmallCanon");
                }
                isReturn = true; // ������ ��������
                isMoving = false;
                //Destroy(gameObject);
            }
        }
        else if (isReturn)
        {
            transform.up = Vector3.Lerp(transform.up, (targetPosition - playerObj.transform.position).normalized, Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, speed * Time.deltaTime);

            currentSpeed += acceleration * Time.deltaTime; // �ð��� �������� �ӵ� ����
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, currentSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerObj.transform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // ���� �浹�ϸ�
        {
            enemy = other.gameObject;
        }
        if (other.CompareTag("Island")) // ���� �浹�ϸ�
        {
            isReturn = true;
            isMoving = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // ���� �浹�ϸ�
        {
            enemy = null;
        }
    }
}
