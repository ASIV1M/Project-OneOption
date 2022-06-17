using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColors : MonoBehaviour
{
    private List<Material> _materials = new List<Material>();

    private void Awake()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            foreach (Material material in smr.materials)
            {
                _materials.Add(material);
            }
        }
    }


    private void Start()
    {
        GameEvents.Instance.RandomColorModel += DoRandomColorModel;    
    }


    private void OnDestroy()
    {
        GameEvents.Instance.RandomColorModel -= DoRandomColorModel;
    }


    private void DoRandomColorModel()
    {
        foreach (Material material in _materials)
        {
            material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }


}
