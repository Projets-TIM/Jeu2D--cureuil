using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPompon : MonoBehaviour

{
    public GameObject cibleASuivre;

    // Limites de mouvement de la cam�ra
    public float limiteGauche;
    public float limiteDroite;
    public float limiteHaut;
    public float limiteBas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // R�cup�re la position actuelle de l'objet cible

        Vector3 laPosition = cibleASuivre.transform.position;

        // Limite la position de la cible � l'int�rieur des limites
        if (laPosition.x < limiteGauche) laPosition.x = limiteGauche;           
        if (laPosition.x > limiteDroite) laPosition.x = limiteDroite;
        if (laPosition.y < limiteBas) laPosition.y = limiteBas;
        if (laPosition.y > limiteHaut) laPosition.y = limiteHaut;

        // D�finit la position Z de la cam�ra
        laPosition.z = -10f;

        // Met � jour la position de la cam�ra pour suivre la cible
        transform.position = laPosition;
    }
}
