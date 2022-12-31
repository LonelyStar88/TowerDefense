using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private EnemyController enemyController;

    [SerializeField] private int towerBuildgold = 50; // 타워 건설에 사용되는 골드
    [SerializeField] private PlayerGold playerGold; // Player의 골드량

    public void TowerSpawn(Transform towertiletrans)
    {
        // 타워 건설 가능 여부 확인
        // 돈이 없는 경우(또는 부족한 경우) 건설 X
        if(towerBuildgold > playerGold.CurGold)
        {
            return;
        }
        Tile tile = towertiletrans.GetComponent<Tile>();
        // 이미 건설되어 있는경우
        if (tile.isBuildTower)
        {
            return;
        }
        // 해당 위치에 건설이 안되어있는 경우
        else
        {
            //타워가 건설되어 있음으로 설정
            tile.isBuildTower = true;
            //타워 건설에 필요한 골드만큼 감소
            playerGold.CurGold -= towerBuildgold;
            // 선택한 타일의 위치에 타워 생성
            GameObject obj = Instantiate(tower, towertiletrans.position, Quaternion.identity); //타워 생성을 Tilemap에 깔아둔 TowerTile에만 설치할수있도록 제어
            // 타워 무기에 적 정보 전달
            obj.GetComponent<TowerAttack>().Setup(enemyController);
        }
    }
}
