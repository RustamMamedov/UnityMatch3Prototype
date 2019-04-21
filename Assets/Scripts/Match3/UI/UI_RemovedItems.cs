using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_RemovedItems : UIObserver
{
    [SerializeField] TMP_Text turnsText;
    [SerializeField] IntegerValue removedItems;
    private int previousTurnsMadeValue;

    public void OnUpdate()
    {
        if (previousTurnsMadeValue == removedItems.Value)
            return;

        UpdateTurnsMadeText();
    }

    private void UpdateTurnsMadeText()
    {
        turnsText.text = removedItems.Value.ToString();
    }

}
