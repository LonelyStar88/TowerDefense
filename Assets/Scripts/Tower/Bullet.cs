using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private int damage;

    public void Setup(Transform target, int dam)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
        this.damage = dam;
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null) // Ÿ���� ������ ���
        {
            // �߻�ü�� target�� ��ġ�� �̵� => Translate���� ������?
            Vector3 dir = (target.position - transform.position).normalized;
            movement2D.MoveTo(dir);
        }
        else // Ÿ���� �������� ���� ���
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �ƴ� ���� �ε�����
        if (!collision.CompareTag("Enemy"))
            return;

        // ���� target�� ���� �ƴ� ��
        if (collision.transform != target)
            return;

        // �� ���
        //collision.GetComponent<Enemy>().Die();
        collision.GetComponent<EnemyHP>().Damaged(damage);

        //źȯ ����
        Destroy(gameObject);
    }
}
