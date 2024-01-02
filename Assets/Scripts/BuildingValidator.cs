using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingValidator : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    List<Collider> blockingColliders;

    void Awake()
    {
        blockingColliders = new List<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh.material.color = Color.red;
        BuildingHandler.Instance.IsInBuildableLocation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;
        mesh.material.color = Color.red;
        BuildingHandler.Instance.IsInBuildableLocation = false;
        blockingColliders.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Ground"))
            return;
        blockingColliders.Remove(other);
        if (blockingColliders.Count != 0)
            return;
        mesh.material.color = Color.green;
        BuildingHandler.Instance.IsInBuildableLocation = true;
    }
}
