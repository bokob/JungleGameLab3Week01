using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Spear : MonoBehaviour
{
    public float speed = 6f; // �̵� �ӵ�
    public float returnSpeed = 6f;
    public Vector3 targetPosition; // Ŭ���� ��ġ
    public Vector3 startPosition; // ���� ��ġ 
    public GameObject tail;    // �� ���� �κ�

    public float acceleration = 2f; // ���ӵ�
    private float currentSpeed = 0f; // ���� �ӵ�

    private Vector3 originalScale; // ���� ũ��
    private bool isMoving = false;
    private bool isReturn = false;
    private float reloadingTime;

    GameObject enemy;
    GameObject playerObj;

    Transform[] points = new Transform[2];

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0f); // 3D ��ǥ�� 2D�� ����
        returnSpeed = returnSpeed * StateManager.Instance.ReloadingTime();
        SetTail();

        isMoving = true;
    }

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
            transform.up = (targetPosition - startPosition).normalized; // �ۻ� ��ü ����

            // �̵�
            transform.position = Vector3.Lerp(transform.position, targetPosition,speed * Time.deltaTime);

            // ������ �����ϸ� ����
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                    SoundManager.instance.PlaySFX("Clash");
                }
                else
                {
                    SoundManager.instance.PlaySFX("SmallCanon");
                }
                isReturn = true; // ������ ��������
                isMoving = false;
                //Destroy(gameObject);
            }
        }
        else if (isReturn)
        {
            transform.up = Vector3.Lerp(transform.up, (targetPosition - playerObj.transform.position).normalized, Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, returnSpeed * Time.deltaTime);


            SpearRotation();

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
        if (other.CompareTag("Obstacle")) // ���� �浹�ϸ�
        {
            isMoving = false;
            isReturn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // ���� �浹�ϸ�
        {
            enemy = null;
        }
    }
    void SpearRotation()
    {
        Vector2 direction = transform.position - playerObj.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
