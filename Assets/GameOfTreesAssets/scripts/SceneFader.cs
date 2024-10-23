using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SceneFader : MonoBehaviour
{
	public List<Image> fadeImage = new List<Image>();
	public float fadeSpeed = 1.0f;
	bool called = false;
	private void Start()
	{
		print("Test");
		foreach (GameObject ent in GameObject.FindGameObjectsWithTag("FadingImage"))
		{
			fadeImage.Add(ent.GetComponent<Image>());
		}

		print("fadeImage.Count: " + fadeImage.Count);
		if (fadeImage != null)
		{
			StartCoroutine(FadeIn());
		}

	}

	public void FadeToScene(string sceneName)
	{
		if (called)
			return;
		if (fadeImage != null)
		{
			called = true;

			StartCoroutine(FadeOut(sceneName));
		}
	}

	private IEnumerator FadeIn()
	{
		foreach (Image fade in fadeImage)
		{

			if (fade != null)
			{
				fade.gameObject.SetActive(true);
				fade.canvasRenderer.SetAlpha(1.0f);

				fade.CrossFadeAlpha(0.0f, fadeSpeed, false);
				yield return new WaitForSeconds(fadeSpeed);
				fade.gameObject.SetActive(false);
			}
		}
	}

	private IEnumerator FadeOut(string sceneName)
	{
		foreach (Image fade in fadeImage)
		{
			if (fade != null)
			{
				fade.gameObject.SetActive(true);

				fade.canvasRenderer.SetAlpha(0.0f); // Start with fully transparent

				fade.CrossFadeAlpha(1.0f, fadeSpeed, false); // Fade to fully opaque

				yield return new WaitForSeconds(fadeSpeed * 3);
				SceneManager.LoadScene(sceneName);
				called = false;

			}
		}
	}
}
