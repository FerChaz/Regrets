using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLimbo : MonoBehaviour
{
    public LimboInfo limboInfo;

    public PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // CAMBIAR POSICION A LA QUE ESTABA, HAY QUE VER CON LOS PINCHES
    // DESCARGAR LA ESCENA DE LIMBO
    // ACTIVAR LOS ENEMIGOS
    // DAR UNOS SEGUNDOS DE INVULNERABILIDAD Y ALGUNA ANIMACION

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = limboInfo.deathPosition;
        }
    }

}
