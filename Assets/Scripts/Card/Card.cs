using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Image frontImage;
	[SerializeField] Image backImage;
	[SerializeField] RectTransform rectTransform;
	[SerializeField] RectTransform maskRectTransform;

	[Header("Specs")]
	[SerializeField] float rotationSpeed;

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
		Manager.Card.CardParent.gameObject.SetActive(false);
	}

	private IEnumerator RotateCard()
	{
		yield return new WaitUntil(() => Manager.Card.CardParent.gameObject.activeSelf == true);

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
}
