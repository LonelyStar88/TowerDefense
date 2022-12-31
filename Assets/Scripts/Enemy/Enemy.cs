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
    int wayPointCnt; // 이동 경로 갯수
    Transform[] wayPoints; // 이동 경로 정보
    int curIdx = 0; // 현재 목표지점 index
    Movement2D movement2D; // 오브젝트 이동 제어
    EnemyController enemycontroller; // 적의 삭제를 Enemycontroller를 통해 삭제

    [SerializeField] private int gold = 10; // 적 사망시 전리품

    public void Setup(EnemyController enemyCtr, Transform[] wPs)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemycontroller = enemyCtr;

        //적 이동 경로 WayPoints 정보 설정
        wayPointCnt = wPs.Length;
        this.wayPoints = new Transform[wayPointCnt];
        this.wayPoints = wPs;

        // 적의 위치를 첫번째 WayPoint 위치로 설정
        transform.position = wPs[curIdx].position;

        // 적 이동/목표지점 설정 Onmove 코루틴
        StartCoroutine("OnMove");
        
    }
    
    IEnumerator OnMove()
    {
        // 다음 이동 방향 설정
        NextMoveTo();

        while(true)
        {
            // 적 오브젝트 회전
            transform.Rotate(Vector3.forward * 10);

            // 적의 현재위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed보다 작을 때 if문 실행
            // Time.deltaTime이 아닌 0.02로 곱해주는 이유는, 프레임에 따라 속도가 빠르면 0.02보다 크게 움직이기때문에
            // if문에 걸리지않고 경로를 이탈하는 경우가 발생하기때문
            if(Vector3.Distance(transform.position, wayPoints[curIdx].position) < 0.02f* movement2D.speed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // 현재 도착하고 아직 이동할 WayPoint가 남아있는 경우
        if(curIdx < wayPointCnt - 1)
        {
            // 적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[curIdx].position;
            // 이동 방향 설정 => 다음 목표 지점(WayPointS)
            curIdx++;
            Vector3 direction = (wayPoints[curIdx].position - transform.position).normalized; // Vector좌표 정규화
            movement2D.MoveTo(direction);
        }
        // 현재 도착한 Waypoint가 Goal밖에 없는 경우
        else
        {
            //적이 Arrive 한경우는 골드 획득X
            gold = 0;
            // 적 오브젝트 삭제
            Die(EnemyDieType.Arrive);
        }
    }
    public void Die(EnemyDieType type)
    {
        //몬스터가 죽을때 바로 삭제되는것이아닌,Enemycontroller에서 삭제하여
        //Enemycontroller의 List에 혼동이 가는것을 방지
        enemycontroller.DestroyEnemy(type, this, gold);
    }
}
