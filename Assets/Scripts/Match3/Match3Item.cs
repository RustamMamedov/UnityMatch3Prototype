using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Match3Item : MonoBehaviour
{
    public int COL, ROW;
    public bool IsMarkedForRemoving {set; get;}
    public bool IsMovingDown {set; get;}
    [SerializeField] private Match3ItemData data;
    public Match3ItemData Data => data;
    public UnityAction<Match3Item> OnClicked;


    public void SetRowAndCol(int row, int col)
    {
        this.ROW = row;        
        this.COL = col;
    }

    public void OnStopMovingDown()
    {
        AnimateBouncing();
    }

    private void AnimateBouncing()
    {
        // animate
    }

    private void OnMouseDown()
    {
        OnClicked?.Invoke(this);
    }
}
