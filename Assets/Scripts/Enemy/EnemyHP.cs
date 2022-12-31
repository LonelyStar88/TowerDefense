using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float maxHP; // 최대 체력
    float curHP; // 현재 체력
    bool isDie = false;
    Enemy enemy;
    SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurHP => curHP;
    
    void Awake()
    {
        curHP = maxHP;
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damaged(float damage)
    {
        // 적의 체력이 감소해서 죽을 상황일 때, 여러 타워의 공격을 동시에 받으면
        // enemy.Die() 함수가 여러번 실행될 수 있다.

        if (isDie == true)
            return; //적이 사망 상태이면 아래의 함수를 실행하지 않는다.

        curHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if(curHP <= 0)
        {
            isDie = true;
            enemy.Die(EnemyDieType.Dead);
        }
    }

    // 적 Hit 시 투명도 변화를 위한 코루틴 함수
    IEnumerator HitAlphaAnimation()
    {
        // 현재 적의 색상을 color에 저장
        Color color = spriteRenderer.color;

        // 적의 투명도를 35%로 설정
        color.a = 0.35f;
        spriteRenderer.color = color;

        // 반짝임 시간
        yield return new WaitForSeconds(0.05f);

        // 적의 투명도를 100%로 설정
        color.a = 1f;
        spriteRenderer.color = color;
    }
   
}
