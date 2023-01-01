using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAtkRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OffAttackRange();
    }

    public void OnAttackRange(Vector3 pos, float range)
    {
        gameObject.SetActive(true);

        // ���� ���� ũ��
        float dim = range * 2f;
        transform.localScale = Vector3.one * dim; // Ÿ���� ���� ������ �������� ��Ÿ���� ����, ����X1�� �Ͽ� ũ�⸦ ��Ÿ��
        // ���� ���� ��ġ
        transform.position = pos;
    }

    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}
