using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarterBox : MonoBehaviour
{
    public float slideDuration = 1.0f;
    public Vector3 targetPosition;
    public bool onScreen = false;
    public GameObject fill1;
    public GameObject fill2;
    public int item1Value;
    public int item2Value;
    public TextMeshPro item1ammvis;
    public TextMeshPro item2ammvis;
    private bool item1 = true;

    private void Start()
    {
        fill1.SetActive(true);
        fill2.SetActive(false);
        item1ammvis.text = (item1Value.ToString());
        item2ammvis.text = (item2Value.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && onScreen == true)
        {
            SceneManager.LoadScene("Space");
        }
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput < 0 && item1)
        {
            item1 = false;
            fill1.SetActive(false);
            fill2.SetActive(true);
        }
        else if (verticalInput > 0 && !item1)
        {
            item1 = true;
            fill1.SetActive(true);
            fill2.SetActive(false);
        }

        if (Input.GetButtonDown("Jump")){
            if (item1)
            {
                int minus = StartData.currencyAmm - item1Value;
                if ( minus < 0)
                {
                    
                }
                else
                {
                    StartData.currencyAmm -= item1Value;
                }
            }
            else
            {
                int minus = StartData.currencyAmm - item2Value;
                if (minus < 0)
                {

                }
                else
                {
                    StartData.currencyAmm -= item2Value;
                }

            }
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
