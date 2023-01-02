using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerAssetData : ScriptableObject
{
    public GameObject tower; // 타워생성을 위한 프리팹
    public Weapon[] weapons; // 레벨별 타워 정보

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite; // 타워 이미지
        public float damage; // 공격력
        public float rate; // 공격 속도
        public float range; // 공격 범위
        public int cost; // 건설 또는 업그레이드시 필요 골드 (0레벨 : 건설, 1레벨 이상 : 업그레이드)
    }
}
