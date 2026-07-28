using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Estado zombie = new Estado();
    [SerializeField] private float radioDetector;
    [SerializeField] private Transform target;
    [SerializeField] private float targetDistancia;

    private void Update()
    {
        DetectarJugador();
        SeleccionarEstado();
    }

    private void SeleccionarEstado() 
    { 
        if(targetDistancia < radioDetector) 
        {
            zombie = Estado.correr;
        }
    
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, Vector3.up, 5);
    }

    private void DetectarJugador() 
    {
        Vector3 distancia = transform.position - target.position;
        targetDistancia = distancia.magnitude;
    
    }
}


public enum Estado
{
    quieto  = 0,
    correr  = 1,
    patrullar = 2
}