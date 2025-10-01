using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlePompon : MonoBehaviour
{
    public float vitesseX;  // Vitesse horizontale actuelle
    public float vitesseXMax;   // Vitesse horizontale maximale
    public float vitesseY;  // Vitesse verticale actuelle (pour le saut)
    public float vitesseSaut;   // Force de saut
    public int compteurVie;

    public AudioClip sonMort;
    public AudioClip sonDegat;

    int compteur = 0;

    public TextMeshProUGUI CompteurPoint;

    bool partieTerminee;  // Indique si la partie est terminée  

    void Start()
    {
    }

    void Update()
    {
        if (!partieTerminee)
        {
            // Déplacements horizontaux
            if (Input.GetKey(KeyCode.D))
            {
                vitesseX = vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                vitesseX = -vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
            }

            // Saut
            if (Input.GetKey(KeyCode.W) && Physics2D.OverlapCircle(transform.position, 1f))
            {
                vitesseY = vitesseSaut;
                GetComponent<Animator>().SetBool("saut", true);
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
            }

            // Animation marche
            GetComponent<Animator>().SetBool("marche", Mathf.Abs(vitesseX) > 0.1f);

            // Appliquer les vitesses au Rigidbody
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Arrêter l'animation marche si collision avec aigle
        if (collision.gameObject.name == "aigle")
            GetComponent<Animator>().SetBool("marche", false);

        // Réinitialiser l'animation de saut si on touche le sol
        if (Physics2D.OverlapCircle(transform.position, 1f))
            GetComponent<Animator>().SetBool("saut", false);

        // Gestion des ennemis
        if (collision.gameObject.tag == "ennemi")
        {
            // Si l'écureuil est en train de descendre (saut)
            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Destroy(collision.gameObject); // Tue l'ennemi
            }
            else
            {
                // Sinon, l'écureuil prend des dégâts
                compteurVie += 1;
            }
        }

        // Collecte des noisettes
        if (collision.gameObject.name == "noisette")
        {
            Destroy(collision.gameObject);
            compteur += 1;
            CompteurPoint.text = "Noix : " + compteur.ToString();
            GetComponent<AudioSource>().PlayOneShot(sonDegat);
        }

        // Gestion des plateformes
        if (collision.gameObject.tag == "plateforme")
            transform.parent = collision.gameObject.transform;

        // Gestion des vies
        if (compteurVie == 1) Destroy(GameObject.Find("coeur1"));
        if (compteurVie == 2) Destroy(GameObject.Find("coeur2"));
        if (compteurVie == 3)
        {
            Destroy(GameObject.Find("coeur3"));
            GetComponent<Animator>().SetBool("mort", true);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            partieTerminee = true;
            Invoke("recommencer", 2f);
        }
    }

    void OnTriggerEnter2D(Collider2D infoCollider)
    {
        if (infoCollider.gameObject.name == "ZoneMortelle")
        {
            partieTerminee = true;
            GetComponent<Animator>().SetBool("mort", true);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            Invoke("recommencer", 2f);
        }
        else if (infoCollider.gameObject.name == "geantNoisette")
        {
            SceneManager.LoadScene("FinalGagne");
        }
    }

    void recommencer()
    {
        SceneManager.LoadScene("FinalMort"); // Redémarre le jeu
    }
}
