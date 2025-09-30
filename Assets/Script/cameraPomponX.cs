using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPomponX : MonoBehaviour
{
    public GameObject CibleASuivre; // Cible à suivre par la caméra

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 maPositionActuelle = transform.position;        // Récupère la position actuelle de la caméra

        maPositionActuelle.x = CibleASuivre.transform.position.x;   // Met à jour la position X de la caméra pour suivre la cible

        transform.position = maPositionActuelle;        // Met à jour la position de la caméra (uniquement la coordonnée X pour l'instant)

    }
}
