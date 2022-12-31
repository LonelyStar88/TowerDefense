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
        if(target != null) // 타겟이 존재할 경우
        {
            // 발사체를 target의 위치로 이동 => Translate와의 차이점?
            Vector3 dir = (target.position - transform.position).normalized;
            movement2D.MoveTo(dir);
        }
        else // 타겟이 존재하지 않을 경우
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적이 아닌 대상과 부딛힐때
        if (!collision.CompareTag("Enemy"))
            return;

        // 현재 target인 적이 아닐 때
        if (collision.transform != target)
            return;

        // 적 사망
        //collision.GetComponent<Enemy>().Die();
        collision.GetComponent<EnemyHP>().Damaged(damage);

        //탄환 제거
        Destroy(gameObject);
    }
}
