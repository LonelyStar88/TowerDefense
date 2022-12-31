using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private EnemyController enemyController;

    [SerializeField] private int towerBuildgold = 50; // Ÿ�� �Ǽ��� ���Ǵ� ���
    [SerializeField] private PlayerGold playerGold; // Player�� ��差

    public void TowerSpawn(Transform towertiletrans)
    {
        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // ���� ���� ���(�Ǵ� ������ ���) �Ǽ� X
        if(towerBuildgold > playerGold.CurGold)
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
            playerGold.CurGold -= towerBuildgold;
            // ������ Ÿ���� ��ġ�� Ÿ�� ����
            GameObject obj = Instantiate(tower, towertiletrans.position, Quaternion.identity); //Ÿ�� ������ Tilemap�� ��Ƶ� TowerTile���� ��ġ�Ҽ��ֵ��� ����
            // Ÿ�� ���⿡ �� ���� ����
            obj.GetComponent<TowerAttack>().Setup(enemyController);
        }
    }
}
