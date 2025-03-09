using System.Collections;
using UnityEngine;

public class SpriteZoomer : MonoBehaviour
{
    [Header("Sprites and Settings")]
    public GameObject[] sprites; // Assign your sprite GameObjects here
    public float[] zoomDurations; // Individual zoom durations for each sprite
    public float zoomAmount = 1.5f; // Scale multiplier during zoom

    private void Start()
    {
        // Ensure all sprites are inactive at the start
        foreach (var sprite in sprites)
        {
            sprite.SetActive(false);
        }

        // Start the sprite display coroutine
        StartCoroutine(DisplaySprites());
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

            // Ensure it reaches the target scale exactly
            sprite.transform.localScale = targetScale;

            // Pause for a moment before hiding
            yield return new WaitForSeconds(0.5f);

            // Reset and deactivate the sprite
            sprite.transform.localScale = originalScale;
            sprite.SetActive(false);
        }
    }
}
