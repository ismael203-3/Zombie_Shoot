using Meta.XR.MRUtilityKit.SceneDecorator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public interface IEstadosZombies
{
    void Accion(NavMeshAgent a);
}

public abstract class Comportamiento
{
    public Transform t { get; set;}
    public Animator animator {  get; set;}
    public float speed;
}


public class Seguir : Comportamiento, IEstadosZombies 
{
    private GameObject zombie;
    public Seguir(Transform target, GameObject zombi, float _speed, Animator animi) 
    { 
        t = target; 
        zombie = zombi; 
        speed = _speed;
        animator = animi;
    }

    public void Accion(NavMeshAgent agent) 
    { 
        agent.SetDestination(t.position);
        agent.speed = speed;
        //zombie.transform.LookAt(t);
        animator.SetFloat("Speed", speed);
    }
}

public class Atacar: Comportamiento, IEstadosZombies
{
    public Atacar(Transform posicion, float _speed, Animator animi) 
    { 
        t = posicion;
        speed = _speed;
        animator = animi;
    }
    public void Accion(NavMeshAgent agent)
    {
        agent.SetDestination(t.position);
        animator.SetFloat("Speed", speed);
    }
}

public class Quieto: Comportamiento, IEstadosZombies 
{
    public Quieto(Transform posicion, Animator animi) 
    { 
        t = posicion;
        animator = animi;
    }
    public void Accion(NavMeshAgent agent)
    {
        agent.SetDestination(t.position);
        animator.SetFloat("Speed", 0);
    }
}