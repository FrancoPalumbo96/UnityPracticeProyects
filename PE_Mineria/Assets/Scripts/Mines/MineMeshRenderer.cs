using UnityEngine;

public class MineMeshRenderer : MonoBehaviour
{
    [SerializeField] private Material _mineMaterial;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial(Color resourceColor)
    {
        _meshRenderer.material = _mineMaterial;

        _meshRenderer.material.color = resourceColor;
    }

    public void ChangeColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
