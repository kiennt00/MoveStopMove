using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearView : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material transparentMaterial;
    [SerializeField] int characterLayer;

    public void TransparentMaterial()
    {
        mesh.material = transparentMaterial;
    }

    public void NormalMaterial()
    {
        mesh.material = normalMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == characterLayer)
        {
            Character character = Cache.Ins.GetCachedComponent<Character>(other);
            if (character is Player)
            {
                TransparentMaterial();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == characterLayer)
        {
            Character character = Cache.Ins.GetCachedComponent<Character>(other);
            if (character is Player)
            {
                NormalMaterial();
            }
        }
    }
}
