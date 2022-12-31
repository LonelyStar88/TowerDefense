using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //타일 하나에 2개이상의 중복된 타워가 건설되는걸 방지하기 위한 Script

    public bool isBuildTower { set; get; }

    void Awake()
    {
        isBuildTower = false; 
    }
}
