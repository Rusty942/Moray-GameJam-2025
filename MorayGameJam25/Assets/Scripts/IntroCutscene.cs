using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteZoomer : MonoBehaviour
{
    [Header("Sprites and Settings")]
    public GameObject[] sprites;
    public float[] zoomDurations;
    public float zoomAmount = 1.5f;
    public string sceneToLoad;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        foreach (var sprite in sprites)
        {
            sprite.SetActive(false);
        }

        StartCoroutine(DisplaySprites());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private IEnumerator DisplaySprites()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            var sprite = sprites[i];
            sprite.SetActive(true);

            Vector3 originalScale = sprite.transform.localScale;
            Vector3 targetScale = originalScale * zoomAmount;

            float elapsedTime = 0f;

            // Zoom-in animation
            while (elapsedTime < zoomDurations[i])
            {
                sprite.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / zoomDurations[i]);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            sprite.transform.localScale = targetScale;

            yield return new WaitForSeconds(0.5f);

            sprite.transform.localScale = originalScale;
            sprite.SetActive(false);
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
