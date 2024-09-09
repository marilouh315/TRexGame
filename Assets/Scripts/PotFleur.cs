using UnityEngine;

public class PotFleur : MonoBehaviour
{
    [SerializeField] private GameObject potInitial;
    [SerializeField] private GameObject potCasse;

    public void OnCollisionEnter(Collision collision)
    {
        Vector3 impulsion = 0.1f * collision.impulse;

        if (potInitial != null)
        {
            Destroy(potInitial.gameObject); //Faire disparaitre le pot initial (gameobject au complet)
            GameObject nouveauPot = Instantiate(potCasse, transform);
            Rigidbody[] morceaux = nouveauPot.GetComponentsInChildren<Rigidbody>();


            foreach (Rigidbody rigidbodyMorceau in morceaux)
            {
                rigidbodyMorceau.AddForce(impulsion, ForceMode.Impulse);
            }
        }

    }

}
