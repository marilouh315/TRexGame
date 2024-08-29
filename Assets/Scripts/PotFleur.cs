using UnityEngine;

public class PotFleur : MonoBehaviour
{
    [SerializeField] private GameObject potInitial;
    [SerializeField] private GameObject potCasse;

    public void OnCasser()
    {
        if (potInitial != null)
        {
            Destroy(potInitial.gameObject); //Faire disparaitre le pot initial (gameobject au complet)
        }
        GameObject nouvelObjet = Instantiate(potCasse, transform);
    }

}
