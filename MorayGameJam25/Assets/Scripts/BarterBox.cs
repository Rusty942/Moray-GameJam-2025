using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarterBox : MonoBehaviour
{
    public float slideDuration = 1.0f;
    public Vector3 targetPosition;
    public bool onScreen = false;

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && onScreen == true)
        {
            SceneManager.LoadScene("Space");
        }
    }

    public IEnumerator SlideInBarterBox()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        onScreen = true;
    }
}
