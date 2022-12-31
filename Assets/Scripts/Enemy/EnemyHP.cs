using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float maxHP; // �ִ� ü��
    float curHP; // ���� ü��
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
        // ���� ü���� �����ؼ� ���� ��Ȳ�� ��, ���� Ÿ���� ������ ���ÿ� ������
        // enemy.Die() �Լ��� ������ ����� �� �ִ�.

        if (isDie == true)
            return; //���� ��� �����̸� �Ʒ��� �Լ��� �������� �ʴ´�.

        curHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if(curHP <= 0)
        {
            isDie = true;
            enemy.Die(EnemyDieType.Dead);
        }
    }

    // �� Hit �� ���� ��ȭ�� ���� �ڷ�ƾ �Լ�
    IEnumerator HitAlphaAnimation()
    {
        // ���� ���� ������ color�� ����
        Color color = spriteRenderer.color;

        // ���� ������ 35%�� ����
        color.a = 0.35f;
        spriteRenderer.color = color;

        // ��¦�� �ð�
        yield return new WaitForSeconds(0.05f);

        // ���� ������ 100%�� ����
        color.a = 1f;
        spriteRenderer.color = color;
    }
   
}
