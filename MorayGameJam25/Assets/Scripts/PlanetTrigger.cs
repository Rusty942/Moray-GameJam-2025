using UnityEngine;
using UnityEngine.SceneManagement;
public class PlanetTrigger : MonoBehaviour
{
    public GameObject landingUi;
    private bool touchingPlanet = false;
    public GameObject buttonFill;
    public string scene;

    private void Start()
    {
        buttonFill.SetActive(false);
    }
    private void Update()
    {
        if (touchingPlanet && Input.GetButtonDown("Submit"))
        {
            buttonFill.SetActive(true);
            SceneManager.LoadScene(scene);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            landingUi.SetActive(true);
            touchingPlanet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            landingUi.SetActive(false);
            touchingPlanet = false;
            buttonFill.SetActive(false);
        }
    }
}