using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image imgscreen; //������ ������ ȭ�� �ִϸ��̼� �̹���
    [SerializeField] private float maxHP = 20f; // �ִ� ü��
    float curHP;

    public float MaxHP => maxHP;
    public float CurHP => curHP;

   
    void Awake()
    {
        curHP = maxHP;
    }

    public void Damaged(float damage)
    {
        curHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        // ü�� 0 ���ϸ� GameOver
        if(curHP <= 0)
        {
            
        }
    }

    IEnumerator HitAlphaAnimation()
    {
        // ��üȭ�� ũ��� ��ġ�� imgscreen�� ������ color ������ ����
        // imgscreen�� ������ 40%�� ����
        Color color = imgscreen.color;
        color.a = 0.4f;
        imgscreen.color = color;

        // ������ 0%�ɶ����� ����

        while( color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imgscreen.color = color;

            yield return null;
        }
    }
}
