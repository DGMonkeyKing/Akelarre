using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    List<Collider> currentHits = new List<Collider>();
    List<Collider> lastHits = new List<Collider>();
    List<Collider> lastHitsToRemove = new List<Collider>();

    void Update()
    {
        Ray ray = MousePointer.GetWorldRay(Camera.main);

        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, MousePointer.RaycastLength);

        foreach(RaycastHit hit in hits)
        {
            currentHits.Add(hit.collider);
        }

        Debug.Log(currentHits.Count);
        
        // Tenemos algún hit del frame anterior?
        if(lastHits.Count > 0)
        {
            foreach(Collider hit in lastHits)
            {
                // Si ese hit del frame anterior no está en los current, entonces se ha salido.
                if(!currentHits.Contains(hit))
                {
                    hit.transform.GetComponent<AbstractInteractable>().OnExit();
                    Debug.Log("ON EXIT");
                    lastHitsToRemove.Add(hit); // Añadimo a lista temporal para luego borrar.
                }
            }

            lastHits.RemoveAll( h => lastHitsToRemove.Contains(h) ); //Borramos todos los contenidos en TMP
        }

        if(currentHits.Count > 0) //Hay hit
        {
            foreach(Collider hit in currentHits)
            {
                //Estamos encima de un nuevo objeto interactable.
                if(!lastHits.Contains(hit)) 
                {
                    hit.transform.GetComponent<AbstractInteractable>().OnOver();
                    Debug.Log("ON OVER");
                    lastHits.Add(hit);
                }
                //Hemos clickado estando encima de un objeto interactable.
                /*if(lastHits.Contains(hit) && MousePointer.GetLeftButtonClickedThisFrame())
                {
                    hit.transform.GetComponent<AbstractInteractable>().OnClick();
                    lastHits.Remove(hit);
                }*/
            }
        }

        lastHitsToRemove.Clear();
        currentHits.Clear();
    }
}
