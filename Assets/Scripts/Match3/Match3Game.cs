using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Game : MonoBehaviour
{
	[SerializeField] private Match3ItemFactory match3ItemFactory;
	[SerializeField] private BoolValue playerCanMakeTurn;
	[SerializeField] private Match3GridParams gridParams;

	[Space, Header("Match 3 Events")]
	[SerializeField] private GameEvent OnTurnMade;
	[SerializeField] private GameEvent OnItemsRemoved;
	
	private Match3Item[][] items;
	private List<Match3Item> selectedForRemovingItems = new List<Match3Item>();

	private void Start()
	{
		InitItems();
	}

	private void InitItems()
	{
		if (match3ItemFactory == null)
		{
			Debug.LogWarning("Please set match 3 item factory");
			return;
		}

		items = new Match3Item[gridParams.ROWS][];
		for (int row = 0; row < gridParams.ROWS; row++)
		{
			items[row] = new Match3Item[gridParams.COLS];
			for (int col = 0; col < gridParams.COLS; col++)
				AddNewItem(row, col, -1);
		}
	}

	private void AddNewItem(int row, int col, int addingRowOffsetInd)
	{
		GameObject newItemObject;
		newItemObject = match3ItemFactory.GetItem();
		newItemObject.transform.parent = transform;

		Match3Item newItem = newItemObject.GetComponent<Match3Item>();
		newItem.IsMarkedForRemoving = false;
		items[row][col] = newItem;
		items[row][col].SetRowAndCol(row, col);
		float startX = col * gridParams.CELL_WIDTH - gridParams.COLS * .5f * gridParams.CELL_WIDTH + gridParams.CELL_WIDTH * .5f;
		float startY = row * gridParams.CELL_WIDTH + (gridParams.ROWS + row + col) * gridParams.CELL_WIDTH;
		if (addingRowOffsetInd > -1) startY = gridParams.ROWS * gridParams.CELL_WIDTH + addingRowOffsetInd * gridParams.CELL_WIDTH;
		items[row][col].transform.localPosition = new Vector2(startX, startY);
		newItem.OnClicked += HandleItemClick;
	}

	public void OnUpdate()
	{
		int itemsMoved;
		updateItemsPositions(out itemsMoved);
		playerCanMakeTurn.Value = itemsMoved == 0;
	}

	private void CreateNewItemsInsteadOfRemoving()
	{
		for (int i = 0; i < selectedForRemovingItems.Count; i++)
			CreateItemsOnColumn(selectedForRemovingItems[i]);
	}

	private void CreateItemsOnColumn(Match3Item item)
	{
		int amountNeededToCreate = 0;
		int lowestEmptyRow = -1;
		Match3Item tempItem;

		for (int row = gridParams.ROWS - 1; row >= 0; row--)
		{
			tempItem = items[row][item.COL];
			if (tempItem.IsMarkedForRemoving)
			{
				amountNeededToCreate++;
				lowestEmptyRow = tempItem.ROW;
			}
		}

		if (amountNeededToCreate == 0)
			return;

		if (lowestEmptyRow > -1)
		{
			int rowOffsetForExistingItems = 0;
			for (int row = lowestEmptyRow; row < gridParams.ROWS; row++)
			{
				tempItem = items[row][item.COL];

				if (tempItem.IsMarkedForRemoving)
				{
					rowOffsetForExistingItems++;
					continue;
				}

				tempItem.ROW -= rowOffsetForExistingItems;
				items[tempItem.ROW][item.COL] = tempItem;
			}
		}

		for (int row = 0; row < amountNeededToCreate; row++)
			AddNewItem(gridParams.ROWS - 1 - row, item.COL, amountNeededToCreate - row);
	}

	private void updateItemsPositions(out int itemsMoved)
	{
		itemsMoved = 0;
		for (int i = 0; i < gridParams.ROWS; i++)
		{
			for (int j = 0; j < gridParams.COLS; j++)
			{
				Match3Item item = items[i][j];

				if (item.IsMarkedForRemoving)
					continue;

				float endY = item.ROW * gridParams.CELL_WIDTH;
				if (item.transform.localPosition.y > endY)
				{
					item.IsMovingDown = true;
					float delta = item.transform.localPosition.y - endY;
					item.transform.localPosition = new Vector2(item.transform.localPosition.x, item.transform.localPosition.y - Mathf.Min(delta, gridParams.fallSpeed));
					itemsMoved++;
				}
				else if (item.transform.localPosition.y == endY && item.IsMovingDown)
				{
                    item.IsMovingDown = false;
                    item.OnStopMovingDown();
				}
			}
		}
	}

	private void HandleItemClick(Match3Item clickedItem)
	{
        if (!playerCanMakeTurn.Value)
            return;

		clickedItem.OnClicked -= HandleItemClick;
		SelectSameTypeCardsAroundClicked(clickedItem.ROW, clickedItem.COL, clickedItem.Data.itemType);
		CreateNewItemsInsteadOfRemoving();
		RemoveSelectedItems();
	}

	private void SelectSameTypeCardsAroundClicked(int row, int col, int itemType)
	{
		if (row < 0 || row >= gridParams.ROWS || col < 0 || col >= gridParams.COLS)
			return;

		if (items[row][col].Data.itemType != itemType)
			return;

		if (items[row][col].IsMarkedForRemoving)
			return;

		items[row][col].IsMarkedForRemoving = true;
		selectedForRemovingItems.Add(items[row][col]);
		SelectSameTypeCardsAroundClicked(row + 1, col, itemType);
		SelectSameTypeCardsAroundClicked(row - 1, col, itemType);
		SelectSameTypeCardsAroundClicked(row, col + 1, itemType);
		SelectSameTypeCardsAroundClicked(row, col - 1, itemType);
	}

	private void RemoveSelectedItems()
	{
		for (int i = selectedForRemovingItems.Count - 1; i >= 0; i--)
			match3ItemFactory.DestroyItem(selectedForRemovingItems[i].gameObject);

		OnTurnMade?.InvokeEvent();
		OnItemsRemoved?.InvokeEvent(new Match3RemovedItems(selectedForRemovingItems));

		selectedForRemovingItems.Clear();
	}	
}
