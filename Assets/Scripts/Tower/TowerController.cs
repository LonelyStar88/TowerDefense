using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private TowerAssetData towerTemp; // Ÿ�� ����(Status)
  //  [SerializeField] private GameObject tower;
    [SerializeField] private EnemyController enemyController;

  //  [SerializeField] private int towerBuildgold = 50; // Ÿ�� �Ǽ��� ���Ǵ� ���
    [SerializeField] private PlayerGold playerGold; // Player�� ��差
    

    public void TowerSpawn(Transform towertiletrans)
    {
        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // ���� ���� ���(�Ǵ� ������ ���) �Ǽ� X
        if(towerTemp.weapons[0].cost > playerGold.CurGold)
        {
            return;
        }
        Tile tile = towertiletrans.GetComponent<Tile>();
        // �̹� �Ǽ��Ǿ� �ִ°��
        if (tile.isBuildTower)
        {
            return;
        }
        // �ش� ��ġ�� �Ǽ��� �ȵǾ��ִ� ���
        else
        {
            //Ÿ���� �Ǽ��Ǿ� �������� ����
            tile.isBuildTower = true;
            //Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
            playerGold.CurGold -= towerTemp.weapons[0].cost;
            // ������ Ÿ���� ��ġ�� Ÿ�� ����
            Vector3 Pos = towertiletrans.position + Vector3.back; // ���� Ÿ���� ������ �������� Ŭ���ϸ� Ÿ���� �켱������ �ɼ��ֵ��� z�ຸ�� -1�� ����
            GameObject obj = Instantiate(towerTemp.tower , Pos, Quaternion.identity); //Ÿ�� ������ Tilemap�� ��Ƶ� TowerTile���� ��ġ�Ҽ��ֵ��� ����
            // Ÿ�� ���⿡ �� ���� ����
            obj.GetComponent<TowerAttack>().Setup(enemyController, playerGold, tile); //Ÿ�� ���׷��̵�ÿ� �ʿ��� ��庸���� �˻� ��
                                                                                //���׷��̵� ���ɽ� ��� ������ ���� �÷��̾��� ��� ������ �ʿ�
        }
    }
}
