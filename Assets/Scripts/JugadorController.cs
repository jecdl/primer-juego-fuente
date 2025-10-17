// using UnityEngine;
// using UnityEngine.InputSystem;

// public class JugadorController : MonoBehaviour
// {
//     private Rigidbody rb;
//     public float velocidad = 10f;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         Debug.Log("JugadorController iniciado correctamente");
//     }

//     void FixedUpdate()
//     {
//         // Leer input directamente del teclado
//         float movimientoH = 0f;
//         float movimientoV = 0f;

//         var keyboard = Keyboard.current;
//         if (keyboard != null)
//         {
//             if (keyboard.wKey.isPressed) movimientoV = 1f;
//             if (keyboard.sKey.isPressed) movimientoV = -1f;
//             if (keyboard.aKey.isPressed) movimientoH = -1f;
//             if (keyboard.dKey.isPressed) movimientoH = 1f;
//         }

//         Vector3 movimiento = new Vector3(movimientoH, 0f, movimientoV);

//         if (movimiento != Vector3.zero)
//         {
//             Debug.Log("Aplicando fuerza: " + movimiento * velocidad);
//         }

//         rb.AddForce(movimiento * velocidad);
//     }
// }

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class JugadorController : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad = 10f;
    private int contador;
    private int contadorAnillos;

    // Variables para los textos
    public TextMeshProUGUI textoContador, textoGanar, textoNombreMatricula;

    // Variables para audio
    public AudioSource audioSourceFX;  // Para efectos de sonido
    public AudioSource audioSourceMusica;  // Para música de ambiente
    public AudioClip sonidoCubo;
    public AudioClip sonidoAnillo;
    public AudioClip musicaAmbiente;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        contadorAnillos = 0;
        Debug.Log("Contador iniciado: " + contador);

        // Actualizo el texto del contador por primera vez
        setTextoContador();

        // Inicio el texto de ganar a vacío
        if (textoGanar != null)
            textoGanar.text = "";

        // Agregamos nombre y matrícula en esquina superior derecha
        if (textoNombreMatricula != null)
            textoNombreMatricula.text = "Josuan\nMatrícula: 1-20-1752";

        // Reproducir música de ambiente al iniciar
        if (audioSourceMusica != null && musicaAmbiente != null)
        {
            audioSourceMusica.clip = musicaAmbiente;
            audioSourceMusica.loop = true;  // Que se repita
            audioSourceMusica.Play();
        }
    }

    void FixedUpdate()
    {
        // Leer input directamente del teclado
        float movimientoH = 0f;
        float movimientoV = 0f;

        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed) movimientoV += 1f;
            if (keyboard.sKey.isPressed) movimientoV -= 1f;
            if (keyboard.aKey.isPressed) movimientoH -= 1f;
            if (keyboard.dKey.isPressed) movimientoH += 1f;
        }

        Vector3 movimiento = new Vector3(movimientoH, 0f, movimientoV);
        rb.AddForce(movimiento * velocidad);
    }

    void OnTriggerEnter(Collider other)
    {
        // Coleccionables tipo CUBO (tag: "Cubo")
        if (other.gameObject.CompareTag("Cubo"))
        {
            Debug.Log("¡Cubo recogido!");
            other.gameObject.SetActive(false);
            contador = contador + 1;

            // Reproducir sonido de cubo (FX)
            if (audioSourceFX != null && sonidoCubo != null)
                audioSourceFX.PlayOneShot(sonidoCubo);

            setTextoContador();
        }

        // Coleccionables tipo ANILLO (tag: "Anillo")
        if (other.gameObject.CompareTag("Anillo"))
        {
            Debug.Log("¡Anillo recogido!");
            other.gameObject.SetActive(false);
            contadorAnillos = contadorAnillos + 1;

            // Reproducir sonido de anillo
            if (audioSourceFX != null && sonidoAnillo != null)
                audioSourceFX.PlayOneShot(sonidoAnillo);

            // PAUSAR la música de fondo cuando recojas anillo
            if (audioSourceMusica != null && audioSourceMusica.isPlaying)
            {
                audioSourceMusica.Pause();
                Debug.Log("¡Música pausada!");
            }

            setTextoContador();
        }
    }

    // Método que actualiza el texto del contador (O muestra el de ganar si las ha cogido todas)
    void setTextoContador()
    {
        if (textoContador != null)
            textoContador.text = "Cubos: " + contador.ToString() + " | Anillos: " + contadorAnillos.ToString();

        // Ganar si recoges 12 objetos TOTALES (cubos + anillos)
        int totalObjetos = contador + contadorAnillos;
        if (totalObjetos >= 12)
        {
            if (textoGanar != null)
                textoGanar.text = "¡GANASTE!";
        }
    }

    // Método público para obtener el contador desde otros scripts
    public int ObtenerContador()
    {
        return contador;
    }

    public int ObtenerContadorAnillos()
    {
        return contadorAnillos;
    }

    /* CÓDIGO VIEJO - USANDO Input.GetAxis
    void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

        rb.AddForce(movimiento * velocidad);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
        }
    }
    */
}