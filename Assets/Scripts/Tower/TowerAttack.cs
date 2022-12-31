using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireState
{
    SearchTarget,
    AttackToTarget
}
public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Bullet; //Bullet������
    [SerializeField] private Transform spawnPoint; // Bullet ������ġ
    [SerializeField] private float AtkRate = 0.5f; // ���� �ӵ�
    [SerializeField] private float AtkRange = 2f; // ���ݹ���
    [SerializeField] private int AtkDamage = 1; // ���ݷ�
    FireState fireState = FireState.SearchTarget; // Ÿ�� �ѱ��� ����
    Transform target = null;
    EnemyController enemyController; // �� ����

    public void Setup(EnemyController enemyCtr)
    {
        this.enemyController = enemyCtr;

        Changestate(FireState.SearchTarget);
    }

    public void Changestate(FireState state)
    {
        // ������ ������̴� ���� ����
        StopCoroutine(fireState.ToString());

        // ���� ����
        fireState = state;

        // ���ο� ���� ���
        StartCoroutine(fireState.ToString());
    }
    

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            RotateToTarget();
        }
    }

    void RotateToTarget()
    {
        // ����(Ÿ���� �߻�ü ������)���κ��� Ÿ���� �ִ� ��ġ���� �Ÿ��� 
        // ���������κ����� ������ �̿��� Ÿ���� ��ġ�� ���ϴ� �� ��ǥ�� �̿�
        float x = target.position.x - transform.position.x;
        float y = target.position.y - transform.position.y;
        // x,y������ ���� ���ϱ�
        float degree = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // ������ Radian(x/360 �����̱⶧���� Mathf.Rad2Deg�� ���� ���� ������ ����
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }
    IEnumerator SearchTarget()
    {
        while(true)
        {
            // ���� ����� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float enemydis = Mathf.Infinity;
            // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
            for(int i = 0; i< enemyController.EnemyList.Count; ++i)
            {
                float dis = Vector3.Distance(enemyController.EnemyList[i].transform.position, transform.position);
                // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ ����� ��� => Ÿ�� ����
                if(dis <= AtkRange && dis <= enemydis)
                {
                    enemydis = dis;
                    target = enemyController.EnemyList[i].transform; 
                }
            }

            if(target != null)
            {
                Changestate(FireState.AttackToTarget);
            }

            yield return null;
        }
    }

    IEnumerator AttackToTarget()
    {
        while(true)
        {
            // target ���� �˻�(�ٸ� bullet�� ���� ���� or Goal���� �̵��� ���� ��)
            if(target == null)
            {
                Changestate(FireState.SearchTarget);
                break;
            }

            // target�� ���� ���� �ȿ� �ִ��� �˻�(���� ������ ����� ���ο� �� Ž��)
            float dis = Vector3.Distance(target.position, transform.position);
            if(dis > AtkRange)
            {
                target = null;
                Changestate(FireState.SearchTarget);
                break;
            }

            // ���� �ӵ���ŭ ������
            yield return new WaitForSeconds(AtkRate);

            // ���� (Bullet ����)
            CreateBullet();
        }
    }

    void CreateBullet()
    {
       GameObject obj =  Instantiate(Bullet, spawnPoint.position, Quaternion.identity); //���� spawnPoint�� ��ġ���� Bullet ����
       obj.GetComponent<Bullet>().Setup(target, AtkDamage); // źȯ���� target ���� ����
    }
}
