using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] //Force l'ajout du component NavMeshAgent si absent

public class Roomba : MonoBehaviour
{
    private Vector3 destination;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = transform.position;
    }

    private Vector3 GenererDestinationAlea()
    {
        return new Vector3(Random.Range(-2.5f, 12.5f), 0.0f, Random.Range(-2.5f, 12.5f)); //Représente des floats aléatoires entre -2.5 et 12.5
    }

    void Update()
    {
        if (!agent.pathPending)
        {
            float distanceDest = Vector3.SqrMagnitude(destination - transform.position);

            if (Mathf.Approximately(agent.velocity.sqrMagnitude, 0.0f) || Mathf.Approximately(distanceDest, 0.0f) || !agent.hasPath)
            {
                destination = GenererDestinationAlea();
                agent.destination = destination;
            }
        }
    }
}
