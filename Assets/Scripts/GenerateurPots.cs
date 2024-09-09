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
                //On ajoute l'emplacement de pot � la liste des emplacements et convertit le transform en EmplacementPot
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
    /// M�thode qui cr�e un pot de fleur � un emplacement al�atoire
    /// </summary>
    public void CreerPot(InputAction.CallbackContext contexte)
    {
        Debug.Log("Creer pot");
        bool aEteCree = false;

        //Si on a atteint le nombre maximum de pots, on ne peut plus en cr�er
        if (potsFleursL.Count >= emplacementsPot.Length || !contexte.started)
        {
            return;

        }

        //Tant qu'on a pas cr�� de pot
        while (!aEteCree)
        {
            //On choisit un emplacement de pot al�atoire
            EmplacementPot emplacement = emplacementsPot[Random.Range(0, emplacementsPot.Length)];

            //Si l'emplacement est d�j� occup�, on en choisit un autre
            if (emplacement.EstOccupe)
            {
                //Retourne au d�but de la boucle
                continue;
            }
            aEteCree = true;

            //On cr�e un nouveau pot
            PotFleur nouveauPot = Instantiate(prototypePot);
            //On le place � l'emplacement choisi al�atoirement
            nouveauPot.transform.position = emplacement.transform.position;
            emplacement.EstOccupe = true;
            //On ajoute le pot � la liste des pots
            potsFleursL.Add(nouveauPot);
        }

        
    }
}
