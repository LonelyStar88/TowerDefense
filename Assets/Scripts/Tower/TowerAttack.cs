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
    [SerializeField] private GameObject Bullet; //Bullet프리팹
    [SerializeField] private Transform spawnPoint; // Bullet 생성위치
    [SerializeField] private float AtkRate = 0.5f; // 공격 속도
    [SerializeField] private float AtkRange = 2f; // 공격범위
    [SerializeField] private int AtkDamage = 1; // 공격력
    FireState fireState = FireState.SearchTarget; // 타워 총구의 상태
    Transform target = null;
    EnemyController enemyController; // 적 정보

    public void Setup(EnemyController enemyCtr)
    {
        this.enemyController = enemyCtr;

        Changestate(FireState.SearchTarget);
    }

    public void Changestate(FireState state)
    {
        // 이전에 재생중이던 상태 종료
        StopCoroutine(fireState.ToString());

        // 상태 변경
        fireState = state;

        // 새로운 상태 재생
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
        // 원점(타워의 발사체 시작점)으로부터 타겟이 있는 위치와의 거리와 
        // 수평축으로부터의 각도를 이용해 타겟의 위치를 구하는 극 좌표계 이용
        float x = target.position.x - transform.position.x;
        float y = target.position.y - transform.position.y;
        // x,y값으로 각도 구하기
        float degree = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // 각도는 Radian(x/360 단위이기때문에 Mathf.Rad2Deg를 곱해 각도 단위를 구함
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }
    IEnumerator SearchTarget()
    {
        while(true)
        {
            // 가장 가까운 적을 찾기 위한 최초 거리를 최대한 크게 설정
            float enemydis = Mathf.Infinity;
            // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
            for(int i = 0; i< enemyController.EnemyList.Count; ++i)
            {
                float dis = Vector3.Distance(enemyController.EnemyList[i].transform.position, transform.position);
                // 현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 가까울 경우 => 타겟 변경
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
            // target 유무 검사(다른 bullet에 의해 제거 or Goal까지 이동해 제거 등)
            if(target == null)
            {
                Changestate(FireState.SearchTarget);
                break;
            }

            // target이 공격 범위 안에 있는지 검사(공격 범위를 벗어나면 새로운 적 탐색)
            float dis = Vector3.Distance(target.position, transform.position);
            if(dis > AtkRange)
            {
                target = null;
                Changestate(FireState.SearchTarget);
                break;
            }

            // 공격 속도만큼 딜레이
            yield return new WaitForSeconds(AtkRate);

            // 공격 (Bullet 생성)
            CreateBullet();
        }
    }

    void CreateBullet()
    {
       GameObject obj =  Instantiate(Bullet, spawnPoint.position, Quaternion.identity); //현재 spawnPoint의 위치에서 Bullet 생성
       obj.GetComponent<Bullet>().Setup(target, AtkDamage); // 탄환에게 target 정보 제공
    }
}
