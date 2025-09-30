using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionCam : MonoBehaviour
{
    public GameObject Camera1;          // Cam�ras � g�rer
    public GameObject Camera2;
    // Start is called before the first frame update
    void Start()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);        // Active la cam�ra 1 et d�sactive la cam�ra 2 par d�faut
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))     // Changement de cam�ra avec la touche 1
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))   // Changement de cam�ra avec la touche 2
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);

        }
    }
}
