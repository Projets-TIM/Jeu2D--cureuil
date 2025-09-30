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

    bool partieTerminee;  // Indique si la partie est termin�e  
 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (!partieTerminee)    // V�rifie si la partie n'est pas termin�e

        {


            if (Input.GetKey(KeyCode.D))    // Se d�placer vers la droite
            {
                vitesseX = vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = false;   // Regarder vers la droite

            }
            else if (Input.GetKey(KeyCode.A))   // Se d�placer vers la gauche
            {
                vitesseX = -vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = true;    // Regarder vers la gauche
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;  // Maintenir la vitesse X actuelle
            }



            if (Input.GetKey(KeyCode.W) && Physics2D.OverlapCircle(transform.position, 1f))   // G�re le saut   V�rifie si on est au sol
            {

                vitesseY = vitesseSaut; // Appliquer la force de saut
                GetComponent<Animator>().SetBool("saut", true); // D�clencher l'animation de saut

            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;  // Maintenir la vitesse Y actuelle
            }

            if (vitesseX > 0.1f || vitesseX < -0.1f)     // Met � jour l'animation en fonction du mouvement
            {

                GetComponent<Animator>().SetBool("marche", true);   // D�clencher l'animation de marche
            }
            else
            {

                GetComponent<Animator>().SetBool("marche", false);  // D�clencher l'animation d'inactivit�
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY); // Met � jour la vitesse du Rigidbody avec les vitesses horizontale et verticale actuelles
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        {
            transform.parent = null;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "aigle")
        {
            GetComponent<Animator>().SetBool("marche", false);
        }

        if (Physics2D.OverlapCircle(transform.position, 1f))
        {
            GetComponent<Animator>().SetBool("saut", false);     // R�initialiser l'animation de saut
        }

        // G�re les collisions avec les ennemis et les objets � collecter
        if (collision.gameObject.name == "fourmi" || collision.gameObject.name == "crocodile" || collision.gameObject.name == "chien" || collision.gameObject.name == "batVol" || collision.gameObject.name == "batRepos" || collision.gameObject.name == "lapin" || collision.gameObject.name == "dino" || collision.gameObject.name == "petitOurs" || collision.gameObject.name == "grosOurs" || collision.gameObject.name == "vulture")  // Collision avec un ennemi

        {
            compteurVie += 1;
            

        }
        

        if (compteurVie == 1)
        {
            Destroy(GameObject.Find("coeur1"));
            
        }
        if (compteurVie == 2)
        {
            Destroy(GameObject.Find("coeur2"));
            
        }
        if (compteurVie == 3)
        {
            Destroy(GameObject.Find("coeur3"));
            GetComponent<Animator>().SetBool("mort", true); // D�clencher l'animation de mort
            GetComponent<AudioSource>().PlayOneShot(sonMort);

            partieTerminee = true;  // D�finir le drapeau de fin de partie

            Invoke("recommencer", 2f);   // Red�marrer le jeu apr�s un d�lai
        }

        




    if (collision.gameObject.name == "noisette" )    // Collecte d'une noisette

        {
            Destroy(collision.gameObject);  // D�truire l'objet noisette
            compteur += 1;
            CompteurPoint.text = "Noix : " + compteur.ToString();
            GetComponent<AudioSource>().PlayOneShot(sonDegat);
        }
       

        if (collision.gameObject.tag == "plateforme")
        {
            transform.parent = collision.gameObject.transform;
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
        SceneManager.LoadScene("FinalMort"); // Charger la sc�ne "IntroJeu" pour red�marrer le jeu
    }
  
}
