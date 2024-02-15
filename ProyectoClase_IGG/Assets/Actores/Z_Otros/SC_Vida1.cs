using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//public class SC_Vida : MonoBehaviour
//{
//    public GameObject[] corazon;
//    private int vida;
//    private bool invencible = false;
//    private bool muerto;
//    public float tiempoInvencible = 1f;

//    private void Start()
//    {
//        vida = corazon.Length;
//    }

//    // Colisiones para controlar el fin de las partidas.
//    void OnTriggerEnter2D(Collider2D other)
//    {
//        //Objeto clase musicController que contiene y maneja el audio del juego.
//        MusicController musicController = FindObjectOfType<MusicController>();

//        if (other.gameObject.CompareTag("Finish"))
//        {
//            Debug.Log("GANASTE");
//            musicController.PlayVictoryMusic();
//            win = true;
//            DetenerNave();
//            StartCoroutine(RestartGameAfterDelay(4.0f)); // Delay de 9 segundos

//        }
//        //if (other.gameObject.CompareTag("Muerte"))
//        //{
//        //    Explota();
//        //    Debug.Log("Explotaste");
//        //    musicController.PlayExplosionSound();
//        //    win = false;
//        //    DetenerNave();
//        //    StartCoroutine(RestartGameAfterDelay(4.0f));


//        //}
//        //else if (other.gameObject && !other.gameObject.CompareTag("Finish") && !other.gameObject.CompareTag("NON"))
//        //{
//        //    Explota();
//        //    Debug.Log("No te salgas!");
//        //    //musicController.PlayDefeatSound();
//        //    musicController.PlayExplosionSound();
//        //    win = false;
//        //    DetenerNave();
//        //    StartCoroutine(RestartGameAfterDelay(4.0f));

//        //}
//    }

//    private IEnumerator RestartGameAfterDelay(float delaySeconds)
//    {
//        // Espera durante el tiempo especificado
//        yield return new WaitForSeconds(delaySeconds);

//        // Cambia la partida
//        if (win == true)
//        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//        }
//        if (win == false)
//        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//        }
//    }




//    void Explota()
//    {
//        // Instancia el sistema de part�culas de la explosi�n en la posici�n de la nave
//        GameObject explosionEffect = Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity);
//        ParticleSystem particles = explosionEffect.GetComponent<ParticleSystem>();
//        if (particles != null)
//        {
//            particles.Play();
//        }
//        Destroy(explosionEffect, particles.main.duration); // Asegura destruir el objeto de part�culas tras la reproducci�n

//        // Desactiva todos los hijos del prefab
//        foreach (Transform child in transform)
//        {
//            //if (child.GetComponent<ParticleSystem>() == null) // Excluye el sistema de part�culas
//            //{
//            child.gameObject.SetActive(false);
//            //}
//        }



//        //// Reproduce el sistema de part�culas de la explosi�n
//        //explosionParticles.Play();

//        //// Oculta la nave desactivando su componente de renderizado
//        //// Para MeshRenderer (usado com�nmente en modelos 3D)
//        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
//        //meshRenderer.enabled = false;

//        // Oculta o desactiva la nave
//        //GetComponent<SpriteRenderer>().enabled = false; // Asume que la nave usa SpriteRenderer
//        // O desactivar completamente el objeto de la nave (incluyendo todos sus componentes)
//        //gameObject.SetActive(false);

//        // Opcional: Destruir la nave despu�s de un delay, si la explosi�n tiene una duraci�n conocida
//        // Destroy(gameObject, explosionParticles.main.duration);
//    }

//    public void DetenerNave()
//    {
//        // Detiene todo movimiento y rotaci�n
//        rb2d.velocity = Vector2.zero; //Para la nave
//        rb2d.angularVelocity = 0f; // Quita velocidad
//        rb2d.isKinematic = true; // Bloquea la fisica

//    }

//    public void RestarVida(int cantidad)
//    {
//        if (!invencible && vida > 0)
//    {
        

//        // Ahora, destruye el coraz�n correspondiente al �ndice de vida actual
//        if (vida > 0) // Aseg�rate de que todav�a hay vidas para destruir un coraz�n
//        {
//            Destroy(corazon[vida - 1].gameObject); // Ajusta el �ndice al correcto despu�s de restar vida
//            StartCoroutine(Invulrenaribilidad());
//            vida -= cantidad; // Resta vida primero
//        }
//    }
//        //if (!invencible && vida > 0)
//        //{
//        //    if (vida < 1)
//        //    {
//        //        Destroy(corazon[0].gameObject);
//        //        StartCoroutine(Invulrenaribilidad());

//        //    }

//        //    if (vida < 2)
//        //    {
//        //        Destroy(corazon[1].gameObject);
//        //        StartCoroutine(Invulrenaribilidad());
//        //    }

//        //    if (vida < 3)
//        //    {
//        //        Destroy(corazon[2].gameObject);
//        //        StartCoroutine(Invulrenaribilidad());
//        //    }
//        //}
//    }

//    IEnumerator Invulrenaribilidad()
//    {
//        invencible = true;
//        yield return new WaitForSeconds(tiempoInvencible);
//        invencible = false;
//    }

//    //void update()
//    //{
//    //    if (muerto = true)
//    //    {
//    //        //Debug.Log("Perdiste");
//    //    }

//    //    //if (vida < 1)
//    //    //{
//    //    //    Destroy(corazon[0].gameObject);

//    //    //}
        
//    //    //if(vida <2)
//    //    //{
//    //    //    Destoy(corazon[1].gameObject);
//    //    //}
        
//    //    //if(vida < 3)
//    //    //{
//    //    //    Destroy(corazon[2].gameObject);
//    //    //}
//    //}

//    //public void RecibeDa�o(int d)
//    //{
//    //    if (vida >= 1) 
//    //    {
//    //        vida -= d;
//    //        Destroy(corazon[vida].gameObject);
            
//    //        if (vida <1)
//    //        {
//    //            muerto = true;
//    //        }
//    //    }
//    //}
//}
