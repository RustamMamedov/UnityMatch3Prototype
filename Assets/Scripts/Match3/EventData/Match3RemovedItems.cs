using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Match3RemovedItems : GameEventData
{
	public List<Match3Item> items;

    public Match3RemovedItems(List<Match3Item> items)
    {
        this.items = items;
    }
}
