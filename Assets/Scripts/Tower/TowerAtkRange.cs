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

        // 공격 범위 크기
        float dim = range * 2f;
        transform.localScale = Vector3.one * dim; // 타워의 공격 범위를 원형으로 나타내기 위해, 지름X1을 하여 크기를 나타냄
        // 공격 범위 위치
        transform.position = pos;
    }

    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}
