using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Necesario para UI como Image
using UnityEngine.SceneManagement; // Necesario para manejar cambios de escena
using System.Diagnostics;

public class SC_Vida : MonoBehaviour
{
    private Image[] corazones; // Array para almacenar las referencias a las im�genes de los corazones en la UI
    public int vida; // Representa la vida actual del jugador (3 corazones)
    public bool invencible = false; // Indica si el jugador es temporalmente invencible (cuando recibe da�o)
    public float tiempoInvencible = 1f; // Tiempo durante el cual el jugador es invencible despu�s de recibir da�o
    private bool win;

    // Prefab de las part�culas de explosi�nes
    [SerializeField] private GameObject particulaExplosionPrefab; 
    [SerializeField] private GameObject particulaGolpePrefab;
    // Componente Rigidbody2D para manejar la f�sica
    private Rigidbody2D rb2d; 

    private void Start()
    {
        // Encuentra el panel que contiene los corazones de la UI y obtiene todas las im�genes (corazones)
        GameObject panelCorazonesObj = GameObject.Find("PanelDeVida");
        corazones = panelCorazonesObj.GetComponentsInChildren<Image>();
        vida = corazones.Length; // La vida inicial es igual al n�mero de corazones

        // Obtiene el componente Rigidbody2D para f�sicas
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Busca el controlador de m�sica en la escena
        MusicController musicController = FindObjectOfType<MusicController>(); 

        // Manejo de colisi�n con objetos con tag Finish
        if (other.gameObject.CompareTag("Finish"))
        {
            musicController.PlayVictoryMusic(); // Reproduce m�sica de victoria
            win = true;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(4.0f)); // Espera antes de reiniciar el juego o cargar siguiente nivel
        }
        // Manejo de colisi�n con objetos con tag Muerte
        else if (other.gameObject.CompareTag("Muerte"))
        {
            ProcesarMuerte(musicController);
        }
        // Otros casos de colisi�n que tambi�n son muerte
        else if (!other.gameObject.CompareTag("Finish") && !other.gameObject.CompareTag("NON"))
        {
            ProcesarMuerte(musicController);
        }
    }

    private IEnumerator RestartGameAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(win ? SceneManager.GetActiveScene().buildIndex + 1 : SceneManager.GetActiveScene().buildIndex);
    }

    void Explota()
    {
        GameObject explosionEffect = Instantiate(particulaExplosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem particles = explosionEffect.GetComponent<ParticleSystem>();
        if (particles != null) particles.Play();
        Destroy(explosionEffect, particles.main.duration);
        foreach (Transform child in transform) child.gameObject.SetActive(false); // Desactiva todos los hijos para simular la destrucci�n
    }

    public void DetenerNave()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        rb2d.isKinematic = true; // Hace la nave inm�vil
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Genera part�culas en el punto de colisi�n m�s reciente
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D lastContact = collision.contacts[collision.contacts.Length - 1];
            Instantiate(particulaGolpePrefab, lastContact.point, Quaternion.identity);
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Muerte")
        {
            RestarVida(1);
        }
    }

    public void RestarVida(int cantidad)
    {
        if (!invencible && vida > 0)
        {
            MusicController musicController = FindObjectOfType<MusicController>();
            vida -= cantidad;
            vida = Mathf.Max(0, vida); // Asegura que la vida no sea negativa
            if (vida >= 0)
            {
                musicController.PlayDamange();
                corazones[vida].gameObject.SetActive(false); // Oculta el coraz�n correspondiente
                StartCoroutine(Invulrenaribilidad());
            }
            else
            {
                ProcesarMuerte(musicController);
            }
        }
    }

    private void ProcesarMuerte(MusicController musicController)
    {
        if (vida <= 0)
        {
            DetenerNave();
            Explota();
            musicController.PlayExplosionSound();
            win = false;
            StartCoroutine(RestartGameAfterDelay(4.0f));
        }
    }

    IEnumerator Invulrenaribilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempoInvencible);
        invencible = false;
    }
}