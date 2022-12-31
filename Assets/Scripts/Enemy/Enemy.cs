using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDieType
{
    Dead,
    Arrive
}
public class Enemy : MonoBehaviour
{
    int wayPointCnt; // �̵� ��� ����
    Transform[] wayPoints; // �̵� ��� ����
    int curIdx = 0; // ���� ��ǥ���� index
    Movement2D movement2D; // ������Ʈ �̵� ����
    EnemyController enemycontroller; // ���� ������ Enemycontroller�� ���� ����

    [SerializeField] private int gold = 10; // �� ����� ����ǰ

    public void Setup(EnemyController enemyCtr, Transform[] wPs)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemycontroller = enemyCtr;

        //�� �̵� ��� WayPoints ���� ����
        wayPointCnt = wPs.Length;
        this.wayPoints = new Transform[wayPointCnt];
        this.wayPoints = wPs;

        // ���� ��ġ�� ù��° WayPoint ��ġ�� ����
        transform.position = wPs[curIdx].position;

        // �� �̵�/��ǥ���� ���� Onmove �ڷ�ƾ
        StartCoroutine("OnMove");
        
    }
    
    IEnumerator OnMove()
    {
        // ���� �̵� ���� ����
        NextMoveTo();

        while(true)
        {
            // �� ������Ʈ ȸ��
            transform.Rotate(Vector3.forward * 10);

            // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02 * movement2D.MoveSpeed���� ���� �� if�� ����
            // Time.deltaTime�� �ƴ� 0.02�� �����ִ� ������, �����ӿ� ���� �ӵ��� ������ 0.02���� ũ�� �����̱⶧����
            // if���� �ɸ����ʰ� ��θ� ��Ż�ϴ� ��찡 �߻��ϱ⶧��
            if(Vector3.Distance(transform.position, wayPoints[curIdx].position) < 0.02f* movement2D.speed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // ���� �����ϰ� ���� �̵��� WayPoint�� �����ִ� ���
        if(curIdx < wayPointCnt - 1)
        {
            // ���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[curIdx].position;
            // �̵� ���� ���� => ���� ��ǥ ����(WayPointS)
            curIdx++;
            Vector3 direction = (wayPoints[curIdx].position - transform.position).normalized; // Vector��ǥ ����ȭ
            movement2D.MoveTo(direction);
        }
        // ���� ������ Waypoint�� Goal�ۿ� ���� ���
        else
        {
            //���� Arrive �Ѱ��� ��� ȹ��X
            gold = 0;
            // �� ������Ʈ ����
            Die(EnemyDieType.Arrive);
        }
    }
    public void Die(EnemyDieType type)
    {
        //���Ͱ� ������ �ٷ� �����Ǵ°��̾ƴ�,Enemycontroller���� �����Ͽ�
        //Enemycontroller�� List�� ȥ���� ���°��� ����
        enemycontroller.DestroyEnemy(type, this, gold);
    }
}
