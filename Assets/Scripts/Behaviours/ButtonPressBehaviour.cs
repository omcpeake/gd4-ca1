using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=gcJQDtfd9Eg&ab_channel=DapperDino
public class ButtonPressBehaviour : MonoBehaviour, IPointerClickHandler
{
    private Button button;
    public TextMeshProUGUI text;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (button.name == "Ally1")
            {
                BattleManager.instance.SelectUnit(SelectedUnit.ALLY1);                
            }
            if (button.name=="Enemy1")
            {
                BattleManager.instance.SelectUnit(SelectedUnit.ENEMY1);
            }
        }
        text.SetText(BattleManager.instance.SelectedUnitHP().ToString());
    }
}
