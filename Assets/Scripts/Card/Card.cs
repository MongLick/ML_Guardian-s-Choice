using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[Header("Components")]
	[SerializeField] Button button;
	[SerializeField] Image frontImage;
	[SerializeField] Image backImage;
	[SerializeField] RectTransform rectTransform;
	[SerializeField] RectTransform maskRectTransform;
	[SerializeField] PooledObject poolObject;
	[SerializeField] Transform originalParent;

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
		if(isDraw)
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

	public void MoveAndScaleToTarget(Transform target)
	{
		StartCoroutine(MoveAndScaleRoutine(target));
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (isDraw)
		{
			return;
		}

		originalPosition = rectTransform.position;
		originalParent = transform.parent;
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

		foreach (var result in results)
		{
			CardSlot slot = result.gameObject.GetComponent<CardSlot>();
			if (slot != null)
			{
				slot.OnDrop(eventData);
				return;
			}
		}

		rectTransform.position = originalPosition;
		transform.SetParent(originalParent);
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

	private IEnumerator MoveAndScaleRoutine(Transform target)
	{
		button.interactable = false;
		Vector3 startPosition = transform.position;
		Vector3 targetPosition = target.position;
		Vector3 startScale = transform.localScale;
		Vector3 targetScale = new Vector3(0.5f, 0.5f, 1f);

		float duration = 0.5f;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;

			transform.position = Vector3.Lerp(startPosition, targetPosition, t);
			transform.localScale = Vector3.Lerp(startScale, targetScale, t);

			yield return null;
		}

		transform.position = targetPosition;
		transform.localScale = targetScale;

		transform.SetParent(target, true);
		isDraw = false;
		button.interactable = true;
	}
}
