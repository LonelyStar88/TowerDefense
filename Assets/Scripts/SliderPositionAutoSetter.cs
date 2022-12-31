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
        // Silder가 쫓아다닐 target 설정
        targettrans = target;
        recttrans = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
       // 적이 파괴되어 쫓아다닐 대상이 사라지면 Silder도 삭제 
       if(targettrans == null)
        {
            Destroy(gameObject);
            return;
        }

        // 오브젝트의 위치가 갱신된 이후에 Silder도 함께 위치를 설정하도록 하기 위해
        // Update가 아닌 LateUpdate에서 호출

        // 오브젝트 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targettrans.position);

        // 화면 내에서 좌표 + dis 만큼 떨어진 위치를 silder의 위치로 설정
        recttrans.position = screenPos + dis;
    }
   
}
