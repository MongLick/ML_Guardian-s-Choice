using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private enum CardType { Sword, Bow, Spear, Ice, Poison, Lightning, Wall, Upgrade }
	[SerializeField] CardType cardType;

	[Header("Components")]
	[SerializeField] CardSlot cardSlot;
	public CardSlot CardSlot { get { return cardSlot; } set { cardSlot = value; } }
	[SerializeField] Button button;
	[SerializeField] Image frontImage;
	[SerializeField] Image backImage;
	[SerializeField] RectTransform rectTransform;
	[SerializeField] RectTransform maskRectTransform;
	[SerializeField] PooledObject poolObject;

	[Header("Vector")]
	private Vector3 originalPosition;

	[Header("Specs")]
	[SerializeField] float rotationSpeed;
	[SerializeField] int index;
	public int Index { get { return index; } set { index = value; } }
	[SerializeField] bool isDraw;
	public bool IsDraw { get { return isDraw; } set { isDraw = value; } }

	private void Start()
	{
		backImage.gameObject.SetActive(true);
		frontImage.gameObject.SetActive(false);
		maskRectTransform.sizeDelta = new Vector2(0, maskRectTransform.sizeDelta.y);
	}

	public void FlipCard()
	{
		StartCoroutine(RotateCard());
	}

	public void CardChoice()
	{
		if (isDraw)
		{
			for (int i = 0; i < Manager.UI.CardUI.CardPos.Length; i++)
			{
				if (Manager.UI.CardUI.CardPos[i].GetComponentInChildren<Card>() == null)
				{
					Manager.Card.CardChoice(index, i);
					return;
				}
			}
		}
	}

	public void MoveAndScaleToTarget(RectTransform target)
	{
		StartCoroutine(MoveAndScaleRoutine(target));
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(Manager.Card.IsClick)
		{
			Manager.Tile.UpdateWallTile(false);
			Manager.Tile.UpdateFloorTile(false);
			Manager.Card.IsClick = false;
			return;
		}

        if (cardSlot != null)
        {
			Manager.Card.IsClick = true;
			Manager.Card.CurrentCard = this;

			switch (cardType)
			{
				case CardType.Sword:
					Manager.Tile.UpdateWallTile(true);
					Manager.Tile.UpdateFloorTile(false);
					break;
				case CardType.Bow:
					Manager.Tile.UpdateWallTile(true);
					Manager.Tile.UpdateFloorTile(false);
					break;
				case CardType.Spear:
					Manager.Tile.UpdateWallTile(true);
					Manager.Tile.UpdateFloorTile(false);
					break;
				case CardType.Ice:
					Manager.Tile.UpdateWallTile(false);
					Manager.Tile.UpdateFloorTile(true);
					break;
				case CardType.Poison:
					Manager.Tile.UpdateWallTile(false);
					Manager.Tile.UpdateFloorTile(true);
					break;
				case CardType.Lightning:
					Manager.Tile.UpdateWallTile(false);
					Manager.Tile.UpdateFloorTile(true);
					break;
				case CardType.Wall:
					Manager.Tile.UpdateWallTile(false);
					Manager.Tile.UpdateFloorTile(true);
					break;
				case CardType.Upgrade:
					Manager.Tile.UpdateWallTile(true);
					Manager.Tile.UpdateFloorTile(false);
					break;
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (isDraw)
		{
			return;
		}

		originalPosition = rectTransform.position;
		transform.SetParent(Manager.Card.CardParent, false);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (isDraw)
		{
			return;
		}

		Vector3 newPosition = eventData.position;

		if (Manager.Card.CardParent.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera || Manager.Card.CardParent.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
		{
			RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out newPosition);
		}

		rectTransform.position = newPosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (isDraw)
		{
			return;
		}

		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = eventData.position;

		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, results);

		foreach (RaycastResult result in results)
		{
			CardSlot slot = result.gameObject.GetComponent<CardSlot>();
			if (slot != null)
			{
				slot.OnDrop(eventData);
				return;
			}
		}

		rectTransform.position = originalPosition;
		transform.SetParent(cardSlot.transform, false);
		transform.position = cardSlot.transform.position;
	}

	private IEnumerator RotateCard()
	{
		Manager.Card.CardDrawUI.gameObject.SetActive(true);

		float targetRotation = 0f;
		float currentRotation = -180f;
		rectTransform.rotation = Quaternion.Euler(0f, -180f, 0f);

		while (currentRotation < targetRotation)
		{
			currentRotation += rotationSpeed * Time.deltaTime;
			rectTransform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

			float visibleRatio = Mathf.Clamp01((currentRotation + 180f) / 180f);
			float maskWidth = Mathf.Lerp(0, 0, visibleRatio);
			maskRectTransform.sizeDelta = new Vector2(maskWidth, maskRectTransform.sizeDelta.y);

			if (currentRotation < -90f)
			{
				frontImage.gameObject.SetActive(false);
				backImage.gameObject.SetActive(true);
			}
			else
			{
				backImage.gameObject.SetActive(false);
				frontImage.gameObject.SetActive(true);
			}

			yield return null;
		}

		rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	private IEnumerator MoveAndScaleRoutine(RectTransform target)
	{
		button.interactable = false;
		Vector3 startPosition = rectTransform.position;
		Vector2 startSize = rectTransform.sizeDelta;
		Vector3 startScale = rectTransform.localScale;

		Vector3 targetPosition = target.position;
		Vector2 targetSize = target.sizeDelta * 2;
		Vector3 targetScale = new Vector3(0.5f, 0.5f, 1f);

		float duration = 0.5f;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;

			rectTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
			rectTransform.sizeDelta = Vector3.Lerp(startSize, targetSize, t);
			rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

			yield return null;
		}

		rectTransform.position = targetPosition;
		rectTransform.localScale = targetScale;

		rectTransform.SetParent(target, true);
		isDraw = false;
		button.interactable = true;
	}
}
