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

    // Variables para los textos (compatible con Text y TextMeshPro)
    public TextMeshProUGUI textoContador, textoGanar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        Debug.Log("Contador iniciado: " + contador);

        // Actualizo el texto del contador por primera vez
        setTextoContador();

        // Inicio el texto de ganar a vacío
        if (textoGanar != null)
            textoGanar.text = "";
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
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            Debug.Log("¡Coleccionable recogido!");
            other.gameObject.SetActive(false);
            contador = contador + 1;
            Debug.Log("Total de coleccionables recogidos: " + contador);

            // Actualizo el texto del contador
            setTextoContador();
        }
    }

    // Método que actualiza el texto del contador (O muestra el de ganar si las ha cogido todas)
    void setTextoContador()
    {
        if (textoContador != null)
            textoContador.text = "Contador: " + contador.ToString();

        if (contador >= 12)
        {
            if (textoGanar != null)
                textoGanar.text = "¡Ganaste!";
        }
    }

    // Método público para obtener el contador desde otros scripts
    public int ObtenerContador()
    {
        return contador;
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