using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    private void Start()
    {
        buttonFill.SetActive(true);
    }

    public GameObject buttonFill;
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            buttonFill.SetActive(false);
            SceneManager.LoadScene("IntroCutscene");
        }
    }
}
