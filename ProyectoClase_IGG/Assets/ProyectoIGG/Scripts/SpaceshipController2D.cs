using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using System;

public class SpaceshipController2D : MonoBehaviour
{
    // Declaramos las variables principales publicas para poderlas editar en el viewport.
    public float thrustForce = 5.0f; // Fuerza empuje impulso de la nave.
    public float rotationSpeed = 100.0f; // Velocidad de giro de la nave
    private Boolean win; // Para controlar la victoria
    private int indexScena = 1; // Para el cambiar de nivel modularmente.
    //Variable de las fisicas 2d para el movimiento 
    private Rigidbody2D rb2d;
    //Variable para reproducir los sonidos de los motores
    public AudioClip thrustSound; // Audio para cuando se activen los motores traseros.
    public AudioClip sideThrustSound; // Audio para cuando se activen los motores delanteros.
    //Variables para insertar audios de los motores
    private AudioSource audioEngineThrust; // Audio para cuando se activen los motores traseros.
    private AudioSource audioEngineSide; // Audio para cuando se activen los motores delanteros.
    //Variables para las particulas de los motores.
    public ParticleSystem thrustParticles1; // Particulas de motor trasero derecho
    public ParticleSystem thrustParticles2; // Particulas de motor trasero izquiero
    public ParticleSystem leftParticles; // Particulas de motor delantero derecho
    public ParticleSystem rightParticles; // Particulas de motor delantero izquierdo
    // Referencia al Animator del panel de fade(no funciono)
    public Animator fadeAnimator;

    // Inicialización del componente Rigidbody2D y configuración de los componentes de audio.
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        audioEngineThrust = gameObject.AddComponent<AudioSource>();
        audioEngineThrust.clip = thrustSound;
        audioEngineThrust.loop = true;

        
        audioEngineSide = gameObject.AddComponent<AudioSource>();
        audioEngineSide.clip = sideThrustSound;
        audioEngineSide.loop = true; 
    }

    void Update()
    {
        
        // Control del audio y del sistema de partículas flechas pulsadas.
        
        // Arriba
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
                audioEngineThrust.Play();
                thrustParticles1.Play();
                thrustParticles2.Play();
            
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            audioEngineThrust.Stop();
            thrustParticles1.Stop();
            thrustParticles2.Stop();
        }
        // Izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftParticles.Stop();
            audioEngineSide.Stop();
        }
        // Derecha
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightParticles.Stop();
            audioEngineSide.Stop();
        }

        // Abajo
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            leftParticles.Play();
            rightParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            leftParticles.Stop();
            rightParticles.Stop();
            audioEngineSide.Stop();
        }
    }

    void FixedUpdate()
    {
        // Metodo para actualizar el impulso por la flecha pulsadas.
        Thrust();
        // Metodo para actualizar la rotacion segun la flecha pulsadas.
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 forwardDirection = transform.up;
            rb2d.AddForce(forwardDirection * thrustForce);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 backwardDirection = -transform.up;
            rb2d.AddForce(backwardDirection * thrustForce);
        }
    }

    void Rotate()
    {
        float rotationInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(rb2d.rotation - rotationInput);
    }

    // Colisiones para controlar el fin de las partidas.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Objeto clase musicController que contiene y maneja el audio del juego.
        MusicController musicController = FindObjectOfType<MusicController>();
        
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("GANASTE");
            musicController.PlayVictoryMusic();
            win = true;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(6.0f)); // Delay de 9 segundos

        }
        if (other.gameObject.CompareTag("Muerte"))
        {
            Debug.Log("Explotaste");
            musicController.PlayExplosionSound();
            win = false;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(6.0f));


        }
        else if (other.gameObject && !other.gameObject.CompareTag("Finish") && !other.gameObject.CompareTag("NON"))
        {
            Debug.Log("No te salgas!");
            musicController.PlayDefeatSound();
            win = false;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(6.0f));

        }
    }

    private IEnumerator RestartGameAfterDelay(float delaySeconds)
    {
        // Espera durante el tiempo especificado
        yield return new WaitForSeconds(delaySeconds);

        // Cambia la partida
        if (win == true)
        {
            SceneManager.LoadScene(indexScena++);
        }
        if(win == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void DetenerNave()
    {
        // Detiene todo movimiento y rotación
        rb2d.velocity = Vector2.zero; //Para la nave
        rb2d.angularVelocity = 0f; // Quita velocidad
        rb2d.isKinematic = true; // Bloquea la fisica

    }

    void GanarPartida()
    {
        // Activa la animación de fade que no funciona
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeIn");
        }

    }
}



