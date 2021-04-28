using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMarineColor : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> renderers;

    public void ChangeColors(Material mat)
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material = mat;
        }
    }
}
