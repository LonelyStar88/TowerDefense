using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Ÿ�� �ϳ��� 2���̻��� �ߺ��� Ÿ���� �Ǽ��Ǵ°� �����ϱ� ���� Script

    public bool isBuildTower { set; get; }

    void Awake()
    {
        isBuildTower = false; 
    }
}
