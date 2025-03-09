using UnityEngine;
using UnityEngine.SceneManagement;
public class PlanetTrigger : MonoBehaviour
{
    public GameObject landingUi;
    private bool touchingPlanet = false;
    private bool enterPlanet = false;
    public GameObject buttonFill;
    public string scene;
    public GameObject player;
    public ShipFuel shipFuel;
    public AudioSource beep;

    private void Start()
    {
        buttonFill.SetActive(false);
    }
    private void Update()
    {
        if (touchingPlanet && Input.GetButtonDown("Submit"))
        {
            beep.Play();
            buttonFill.SetActive(true);
            enterPlanet = true;
            SceneManager.LoadScene(scene);
            StartData.playerSpawnLocation = player.transform.position;
            StartData.fuelAmm = StartData.fuelAmm = shipFuel.currentFuel;
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
        if (other.CompareTag("Player") && enterPlanet == false)
        {
            landingUi.SetActive(false);
            touchingPlanet = false;
            buttonFill.SetActive(false);
        }
        else
        {

        }
    }
}