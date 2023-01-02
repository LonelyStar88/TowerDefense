using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image imgscreen; //������ ������ ȭ�� �ִϸ��̼� �̹���
    [SerializeField] private float maxHP = 20f; // �ִ� ü��
    [SerializeField] private TMP_Text GameOverTxt;
    [SerializeField] private Button ReTryBtn;
    [SerializeField] private Button ExitBtn;
    float curHP;
    float delayTime = 0f;

    public bool isGameOver;
    public float MaxHP => maxHP;
    public float CurHP => curHP;

   
    void Awake()
    {
        isGameOver = false;
        curHP = maxHP;
        GameOverTxt.gameObject.SetActive(false);
        ReTryBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
    }

    void Update()
    {
      if(isGameOver)
      {
            delayTime += Time.deltaTime;
      }
    }

    public void Damaged(float damage)
    {
        curHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        // ü�� 0 ���ϸ� GameOver
        if(curHP <= 0)
        {
            isGameOver = true;
            GameOverTxt.gameObject.SetActive(true);
            if (delayTime > 2f)
            {
                ReTryBtn.gameObject.SetActive(true);
                ExitBtn.gameObject.SetActive(true);
            }
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
