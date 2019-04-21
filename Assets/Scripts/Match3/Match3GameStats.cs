using System.Collections.Generic;
using UnityEngine;

public class Match3GameStats : MonoBehaviour
{
	[SerializeField] private IntegerValue TurnsMade;
	[SerializeField] private IntegerValue ItemsRemoved;

	private void Start()
	{
        if (TurnsMade == null)
        {
            Debug.LogWarning("Please set TurnsMade param");
            return;
        }
            
        if (ItemsRemoved == null)
        {
            Debug.LogWarning("Please set ItemsRemoved param");
            return;
        }

		ClearStats();
	}

	private void ClearStats()
	{
		TurnsMade.Value = 0;
		ItemsRemoved.Value = 0;
	}

	public void OnTurnMade()
	{
		TurnsMade.Value++;
	}

	public void OnItemsRemoved(GameEventData eventData)
	{
        Match3RemovedItems removedItemsData = (Match3RemovedItems)eventData;
		ItemsRemoved.Value += removedItemsData.items.Count;
	}
}
