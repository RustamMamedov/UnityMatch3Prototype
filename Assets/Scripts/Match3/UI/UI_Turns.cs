using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Turns : UIObserver
{
    [SerializeField] TMP_Text turnsText;
    [SerializeField] IntegerValue turns;
    private int previousTurnsValue;

    public void OnUpdate()
    {
        if (turns.Value == previousTurnsValue)
            return;
        
        UpdateTurnsText();
    }

    void UpdateTurnsText()
    {
        turnsText.text = turns.Value.ToString();
        previousTurnsValue = turns.Value;
    }
}
