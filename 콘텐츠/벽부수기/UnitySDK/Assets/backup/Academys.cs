using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Academys : Academy
{
    public override void AcademyReset()
    {
        Monitor.SetActive(true);
    }
    // Start is called before the first frame update
   
}
