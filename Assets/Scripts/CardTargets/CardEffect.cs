using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect 
{
    public enum Target 
    {
        Self,
        Single,
        Multiple,
        Select
    }

    public enum Position
    {
        Relative,
        Absolute
    }

    public Position _position;
    //Posición relativa a quien lo ejecute. Números negativos indican izquierda y positivos derecha.
    public int relativePosition; 
    
    
}

