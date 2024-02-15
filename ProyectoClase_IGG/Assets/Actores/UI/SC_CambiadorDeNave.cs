using UnityEngine;

public class SC_CambiadorDeNave : MonoBehaviour
{

    public GameObject[] prefabsDeNaves; // Array p�blico de prefabs de naves para intercambiar entre ellas.
    private Transform  puntoDeSpawn; // Variable privada para almacenar la referencia al punto de spawn en la escena


    void Start()
    {
        // Busca en la escena el objeto "PuntoSpawn" y guardamos su transform
        puntoDeSpawn = GameObject.Find("PuntoSpawn").transform;

        CambiarNave();
    }

    public void CambiarNave()
    {
        // Leemos el �ndice de la nave seleccionada de PlayerPrefs, 0 es el valor por defecto si no se encuentra nada
        int indiceSeleccionado = PlayerPrefs.GetInt("NaveSeleccionada", 0); 

        // Instanciar la nueva nave basada en la selecci�n del dropdown
        if (indiceSeleccionado >= 0 && indiceSeleccionado < prefabsDeNaves.Length)
        {
            // Instanciamos la nueva nave en la posici�n y rotaci�n del punto de spawn
            GameObject nuevaNave = Instantiate(prefabsDeNaves[indiceSeleccionado], puntoDeSpawn.position, puntoDeSpawn.rotation);
            nuevaNave.transform.SetParent(puntoDeSpawn.parent); // Hacemos que la nueva nave sea hija del punto de spawn para mantener la jerarqu�a
        }
    }
}
