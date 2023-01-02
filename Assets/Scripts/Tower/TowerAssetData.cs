using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerAssetData : ScriptableObject
{
    public GameObject tower; // Ÿ�������� ���� ������
    public Weapon[] weapons; // ������ Ÿ�� ����

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite; // Ÿ�� �̹���
        public float damage; // ���ݷ�
        public float rate; // ���� �ӵ�
        public float range; // ���� ����
        public int cost; // �Ǽ� �Ǵ� ���׷��̵�� �ʿ� ��� (0���� : �Ǽ�, 1���� �̻� : ���׷��̵�)
    }
}
