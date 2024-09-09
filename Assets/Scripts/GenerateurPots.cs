using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GenerateurPots : MonoBehaviour
{
    [SerializeField] private PotFleur prototypePot; //Prefab pot //Copie d'un objet existant
    [SerializeField] private EmplacementPot[] emplacementsPot;

    private List<PotFleur> potsFleursL;

    private void Awake()
    {
        potsFleursL = new(); //MEme chose que new List<PotFleur>();
    }

    private void Start()
    {
        emplacementsPot = new EmplacementPot[transform.childCount];
        int indice = 0;
        foreach(Transform enfant in transform)
        {
            if (enfant.TryGetComponent(out EmplacementPot emplacement))
            {
                //On ajoute l'emplacement de pot à la liste des emplacements et convertit le transform en EmplacementPot
                emplacementsPot[indice] = emplacement.GetComponent<EmplacementPot>();
                indice++;

            } 
            else
            {
                Debug.LogError($"L'objet {enfant.name} n'a pas de script EmplacementPot");
            }


        }
    }

    /// <summary>
    /// Méthode qui crée un pot de fleur à un emplacement aléatoire
    /// </summary>
    public void CreerPot(InputAction.CallbackContext contexte)
    {
        Debug.Log("Creer pot");
        bool aEteCree = false;

        //Si on a atteint le nombre maximum de pots, on ne peut plus en créer
        if (potsFleursL.Count >= emplacementsPot.Length || !contexte.started)
        {
            return;

        }

        //Tant qu'on a pas créé de pot
        while (!aEteCree)
        {
            //On choisit un emplacement de pot aléatoire
            EmplacementPot emplacement = emplacementsPot[Random.Range(0, emplacementsPot.Length)];

            //Si l'emplacement est déjà occupé, on en choisit un autre
            if (emplacement.EstOccupe)
            {
                //Retourne au début de la boucle
                continue;
            }
            aEteCree = true;

            //On crée un nouveau pot
            PotFleur nouveauPot = Instantiate(prototypePot);
            //On le place à l'emplacement choisi aléatoirement
            nouveauPot.transform.position = emplacement.transform.position;
            emplacement.EstOccupe = true;
            //On ajoute le pot à la liste des pots
            potsFleursL.Add(nouveauPot);
        }

        
    }
}
