using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPompon : MonoBehaviour

{
    public GameObject cibleASuivre;

    // Limites de mouvement de la caméra
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
        // Récupère la position actuelle de l'objet cible

        Vector3 laPosition = cibleASuivre.transform.position;

        // Limite la position de la cible à l'intérieur des limites
        if (laPosition.x < limiteGauche) laPosition.x = limiteGauche;           
        if (laPosition.x > limiteDroite) laPosition.x = limiteDroite;
        if (laPosition.y < limiteBas) laPosition.y = limiteBas;
        if (laPosition.y > limiteHaut) laPosition.y = limiteHaut;

        // Définit la position Z de la caméra
        laPosition.z = -10f;

        // Met à jour la position de la caméra pour suivre la cible
        transform.position = laPosition;
    }
}
