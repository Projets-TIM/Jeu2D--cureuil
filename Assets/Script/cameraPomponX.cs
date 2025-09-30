using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPomponX : MonoBehaviour
{
    public GameObject CibleASuivre; // Cible � suivre par la cam�ra

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 maPositionActuelle = transform.position;        // R�cup�re la position actuelle de la cam�ra

        maPositionActuelle.x = CibleASuivre.transform.position.x;   // Met � jour la position X de la cam�ra pour suivre la cible

        transform.position = maPositionActuelle;        // Met � jour la position de la cam�ra (uniquement la coordonn�e X pour l'instant)

    }
}
