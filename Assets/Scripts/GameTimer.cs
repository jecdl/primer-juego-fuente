using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public float tiempoTotal = 60f;  // 60 segundos
    private float tiempoRestante;
    private bool juegoActivo = true;

    void Start()
    {
        tiempoRestante = tiempoTotal;
    }

    void Update()
    {
        if (!juegoActivo)
            return;

        // Restar tiempo
        tiempoRestante -= Time.deltaTime;

        // Si se acabó el tiempo
        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            juegoActivo = false;
            GameOver();
        }

        // Actualizar el texto del timer
        ActualizarTextoTiempo();
    }

    void ActualizarTextoTiempo()
    {
        if (textoTiempo != null)
        {
            // Convertir a minutos y segundos
            int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60f);

            textoTiempo.text = string.Format("Tiempo: {0:00}:{1:00}", minutos, segundos);

            // Cambiar color a rojo si quedan menos de 10 segundos
            if (tiempoRestante < 10f)
            {
                textoTiempo.color = Color.red;
            }
        }
    }

    void GameOver()
    {
        Debug.Log("¡TIEMPO TERMINADO! - GAME OVER");

        // Aquí puedes:
        // - Desactivar al jugador
        // - Mostrar un mensaje
        // - Cambiar a escena de Game Over

        // Desactivar el movimiento del jugador
        JugadorController jugador = FindObjectOfType<JugadorController>();
        if (jugador != null)
        {
            jugador.enabled = false;
        }
    }

    public bool EstaJuegoActivo()
    {
        return juegoActivo;
    }

    public float ObtenerTiempoRestante()
    {
        return tiempoRestante;
    }
}
