using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image imgscreen; //데미지 입을시 화면 애니메이션 이미지
    [SerializeField] private float maxHP = 20f; // 최대 체력
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
        // 체력 0 이하면 GameOver
        if(curHP <= 0)
        {
            
        }
    }

    IEnumerator HitAlphaAnimation()
    {
        // 전체화면 크기로 배치된 imgscreen의 색상을 color 변수에 저장
        // imgscreen의 투명도를 40%로 설정
        Color color = imgscreen.color;
        color.a = 0.4f;
        imgscreen.color = color;

        // 투명도가 0%될때까지 감소

        while( color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imgscreen.color = color;

            yield return null;
        }
    }
}
