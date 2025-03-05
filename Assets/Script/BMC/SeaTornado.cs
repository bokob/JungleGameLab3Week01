using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class SeaTornado : Attraction
{
    [SerializeField] Transform trail;
    [SerializeField] Transform trailPrefab;

    [SerializeField] float angle = 5f;
    [SerializeField] float intervalLine = 0.1f;

    [SerializeField] float lifeTime = 5f;

    private void Update()
    {
        //if (Vector3.Distance(trail.transform.position, transform.position) < 0.5f)
        //{
        //    Destroy(trail.gameObject);
        //    trail = Instantiate(trailPrefab, transform);
        //    transform.position = new Vector3(0, 0, 0);
        //    return;
        //}

        trail.transform.RotateAround(transform.position, Vector3.forward, angle);
        trail.position = Vector3.Slerp(trail.transform.position, transform.position, intervalLine * Time.deltaTime);

        transform.Translate(0, 0, 0.1f * Time.deltaTime);

        Destory();
    }

    // �踦 ȸ���� �߽����� ������� ���� ��ȯ
    public override Vector3 GetAttractionDirection(ApplyAttractionObject applyObject)
    {
        // �� -> ȸ���� �߽�
        return (transform.position - applyObject.transform.position).normalized;
    }

    void Destory()
    {
        if (transform.position.z >= 1f)
            Debug.Log("����̵� �Ҹ�!");
    }
}
