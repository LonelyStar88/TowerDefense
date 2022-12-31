using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField] private Vector3 dis = Vector3.down * 20.0f;
    private Transform targettrans;
    private RectTransform recttrans;

    public void Setup(Transform target)
    {
        // Silder�� �Ѿƴٴ� target ����
        targettrans = target;
        recttrans = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
       // ���� �ı��Ǿ� �Ѿƴٴ� ����� ������� Silder�� ���� 
       if(targettrans == null)
        {
            Destroy(gameObject);
            return;
        }

        // ������Ʈ�� ��ġ�� ���ŵ� ���Ŀ� Silder�� �Բ� ��ġ�� �����ϵ��� �ϱ� ����
        // Update�� �ƴ� LateUpdate���� ȣ��

        // ������Ʈ ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targettrans.position);

        // ȭ�� ������ ��ǥ + dis ��ŭ ������ ��ġ�� silder�� ��ġ�� ����
        recttrans.position = screenPos + dis;
    }
   
}
