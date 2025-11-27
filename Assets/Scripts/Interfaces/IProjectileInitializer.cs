using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IProjectileInitializer
{
    public void Initialise(float damage, float speed);
}
