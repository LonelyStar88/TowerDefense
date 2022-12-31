using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    // Player�� ��� ������ ���� Script, Defense Tower�� �Ǽ��� ������������ 
    // Gold ��� �ڿ��� �Ҹ��Ѵ�.
    [SerializeField] private int curGold = 100;
    
    //�ܺο��� ������ �����ϵ��� ������Ƽ ����
    public int CurGold
    {
        set => curGold = Mathf.Max(0, value);
        get => curGold;
    }

    
}
