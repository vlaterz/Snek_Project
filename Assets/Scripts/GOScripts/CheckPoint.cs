using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Color Color;
    private Renderer _meshRenderer;
    void Awake()
    {
        _meshRenderer = GetComponentInChildren<Renderer>();
        var allColors = GameController.GetInstance.GameColors;
        Color = allColors[Random.Range(0, allColors.GetUpperBound(0))];
        _meshRenderer.material.EnableKeyword("_EMISSION");
        _meshRenderer.material.SetColor("_EmissionColor", Color);
    }
}
