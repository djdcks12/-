using UnityEngine;
using MLAgents;
public class My_Academy : Academy
{
    public override void AcademyReset()
    {
        Monitor.SetActive(true);
    }
}
