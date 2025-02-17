using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Image frontImage;
	[SerializeField] Image backImage;
	[SerializeField] RectTransform rectTransform;

	[Header("Specs")]
	[SerializeField] float rotationSpeed;

	private void Start()
	{
		backImage.gameObject.SetActive(true);
		frontImage.gameObject.SetActive(false);
	}

	public void FlipCard()
	{
		StartCoroutine(RotateCard());
	}

	public void CardChoice()
	{
		Manager.Card.CardImage.gameObject.SetActive(false);
	}

	private IEnumerator RotateCard()
	{
		yield return new WaitUntil(() => Manager.Card.CardImage.gameObject.activeSelf == true);

		float targetRotation = 180f;
		float currentRotation = 0f;

		while (currentRotation < targetRotation)
		{
			currentRotation += rotationSpeed * Time.deltaTime;
			rectTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

			if (currentRotation > 90f)
			{
				frontImage.gameObject.SetActive(true);
				backImage.gameObject.SetActive(false);
			}
			else
			{
				backImage.gameObject.SetActive(true);
				frontImage.gameObject.SetActive(false);
			}

			yield return null;
		}
	}
}
