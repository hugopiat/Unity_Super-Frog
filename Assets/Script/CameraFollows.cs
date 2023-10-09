using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollows : MonoBehaviour
{

    public Transform target; // On d�fini une variable publique pour la cible de la cam�ra
    public float smoothing = 5f; // On d�fini une variable publique avec un d�faut pour le smoothing que l'on veut appliquer � la cam�ra
    Vector3 offset; // Propri�t� qui va contenir la diff�rence � maintenir entre la position du personnage ainsi que la position de la cam�ra

    // Use this for initialization

    void Start()
    {
        offset = transform.position - target.position; // Calcul de l'offset � garder entre le personnage et la cam�ra
                                                       // transform est par d�faut disponible dans les classes li� � un game objet
                                                       // ce qui nous permet de le r�up�rer sans avoir besoin d'utiliser GetComponent
                                                       // transform �quivaut � la rubrique transforme au sein de l'inspecteur quand on clique sur un objet
                                                       // L'offset correspondra toujours � la position du personnage et cam�ra dans la sc�ne
    }

    void FixedUpdate() // Fixed Update au lieu de Update pour avoir une interval r�gulier, au lieu de chaque frame
    {
        Vector3 targetCamPosition = target.position + offset; // on d�fini la position o� la cam�ra doit se trouver
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime); // met � jour la position de la cam�ra
                                                                                                              // Ici lerp nous permet de faire une transition lin�aire entre deux point de fa�on fluide
                                                                                                              // Je vous laisse aller voir sur la documentation unity si vous voulez en savoir plus sur cette fonction
                                                                                                              // Ici on utlise Time.deltaTime pour calculer le temps de d�placement * le smoothing
                                                                                                              // Cela nous permet d'avoir une petite latence entre le personnage qui s'arr�te et la cam�ra qui s'arrete sur lui, �a donne de la geule
                                                                                                              // Attention Time.deltaTime repr�sente le temps entre chaque frame
                                                                                                              // Ici on utilise sur fixed update qui intevient � interval r�gulier
                                                                                                              // Cependant sur une fonction update normale, le deltaTime �tant bas� sur les frame, le temps varie selon les moment du jeu
                                                                                                              // deltaTime est � privil�gier dans fixedUpdate au lieu de update au mieux que possible pour �viter les surprises
    }
}
