using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Structure, IProduceUnit
{
    public List<Unit>  producableUnits { get; set; }
    
    public void ProduceDemage(List<Unit> producableUnits)
    {
	    throw new System.NotImplementedException();
    }
}
