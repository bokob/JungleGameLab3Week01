using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Spear : MonoBehaviour
{
    public float speed = 6f; // �̵� �ӵ�
    public float returnSpeed = 3f;
    public Vector3 targetPosition; // Ŭ���� ��ġ
    public Vector3 startPosition; // ���� ��ġ 

    public float acceleration = 2f; // ���ӵ�
    private float currentSpeed = 0f; // ���� �ӵ�

    private Vector3 originalScale; // ���� ũ��
    private bool isMoving = false;
    private bool isReturn = false;
    private float journeyLength;
    private float reloadingTime;

    float distanceCovered = 0f;
    float fractionOfJourney = 0f;

    GameObject enemy;
    GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0f); // 3D ��ǥ�� 2D�� ����
        journeyLength = Vector3.Distance(startPosition, targetPosition);
        isMoving = true;
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
            //transform.up = (targetPosition - playerObj.transform.position).normalized; // �ۻ� ��ü �ݴ���� 
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, speed * Time.deltaTime);

            currentSpeed += acceleration * reloadingTime * Time.deltaTime; // �ð��� �������� �ӵ� ����
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, currentSpeed * Time.deltaTime);
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
