using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    // Player의 골드 관리를 위한 Script, Defense Tower의 건설의 제약조건으로 
    // Gold 라는 자원을 소모한다.
    [SerializeField] private int curGold = 100;
    
    //외부에서 접근이 가능하도록 프로퍼티 선언
    public int CurGold
    {
        set => curGold = Mathf.Max(0, value);
        get => curGold;
    }

    
}
