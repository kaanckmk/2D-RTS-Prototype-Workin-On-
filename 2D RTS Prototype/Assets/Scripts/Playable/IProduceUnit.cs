using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It is an interface that handle producing units of structures. */
public interface IProduceUnit
{
    List<Unit>  producableUnits { get; set; }
    
    void ProduceDemage(List<Unit> producableUnits);

}
