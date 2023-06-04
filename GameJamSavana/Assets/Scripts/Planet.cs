using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : CoreGravity
{
    [SerializeField] float customInfluenceRange;
    [SerializeField] float minCoreForce;
    [SerializeField] float maxCoreForce;
    protected override void Start()
    {
        base.Start();
        // Asignar los valores personalizados a las variables de la clase base
        influenceRange = customInfluenceRange;
        minForce = minCoreForce;
        maxForce = maxCoreForce;
    }
}
