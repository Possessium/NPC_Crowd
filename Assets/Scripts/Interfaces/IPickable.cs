using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    public void PickUp(Transform _parent);
    public void Drop();
}
