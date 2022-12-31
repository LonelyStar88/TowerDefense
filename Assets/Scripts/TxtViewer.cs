using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TxtViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text txtPlayerHP;
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private TMP_Text txtPlayerGold;
    [SerializeField] private PlayerGold playerGold;


    // Update is called once per frame
    void Update()
    {
        txtPlayerHP.text = playerHP.CurHP + "/" + playerHP.MaxHP;
        txtPlayerGold.text = playerGold.CurGold.ToString();
    }
}
