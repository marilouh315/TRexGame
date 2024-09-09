using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerTRex : MonoBehaviour
{
    [SerializeField] private float vitesse;
    [SerializeField] private float vitesseRotation;
    private Vector2 deplacement;
    private float rotation;

    
    public void Deplacer(InputAction.CallbackContext contexte) //Si on veut changer la valeur de la variable d'input, on change (OnDeplacement ou OnMove)
    {
        deplacement = contexte.action.ReadValue<Vector2>();
    }

    public void Rotationner(InputAction.CallbackContext contexte)
    {
        rotation = contexte.action.ReadValue<float>();
    }

    //On va pouvoir mettre le code de déplacement du TRex
    private void FixedUpdate()
    {
        if (deplacement.sqrMagnitude > 0)
        {
            Vector3 deplacementEffectif = (deplacement.y * transform.forward + deplacement.x * transform.right).normalized;
            transform.position += deplacementEffectif * vitesse * Time.deltaTime;
        }
        transform.Rotate(Vector3.up, rotation * vitesseRotation * Time.deltaTime);


    }
}
