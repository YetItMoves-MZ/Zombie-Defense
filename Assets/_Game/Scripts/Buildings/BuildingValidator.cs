using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BuildingValidator : MonoBehaviour
{
    // [SerializeField] MeshRenderer mesh;
    List<Collider> blockingColliders;
    bool isBeyondMaxDistance;
    void Awake()
    {
        blockingColliders = new List<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // mesh.material.color = Color.red;
        if (BuildingHandler.Instance.MaxDistanceFromBase < Vector3.Distance(transform.position, MainBase.Instance.transform.position))
        {
            MakeSolidColor(Color.red, transform);
            BuildingHandler.Instance.IsInBuildableLocation = false;
            isBeyondMaxDistance = true;
        }
        else
        {
            MakeSolidColor(Color.green, transform);
            BuildingHandler.Instance.IsInBuildableLocation = true;
            isBeyondMaxDistance = false;
        }
    }

    void Update()
    {
        if (BuildingHandler.Instance.MaxDistanceFromBase < Vector3.Distance(transform.position, MainBase.Instance.transform.position))
        {
            MakeSolidColor(Color.red, transform);
            BuildingHandler.Instance.IsInBuildableLocation = false;
            isBeyondMaxDistance = true;
        }
        else
        {
            if (blockingColliders.Count == 0)
            {
                MakeSolidColor(Color.green, transform);
                BuildingHandler.Instance.IsInBuildableLocation = true;
                isBeyondMaxDistance = false;
            }
            else
            {
                MakeSolidColor(Color.red, transform);
                BuildingHandler.Instance.IsInBuildableLocation = false;
                isBeyondMaxDistance = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;
        // mesh.material.color = Color.red;
        MakeSolidColor(Color.red, transform);
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

        if (!(BuildingHandler.Instance.MaxDistanceFromBase < Vector3.Distance(transform.position, MainBase.Instance.transform.position)))
        {
            // mesh.material.color = Color.green;
            MakeSolidColor(Color.green, transform);
            BuildingHandler.Instance.IsInBuildableLocation = true;
        }
    }

    void MakeSolidColor(Color color, Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            MakeSolidColor(color, transform.GetChild(i));
        }
        if (transform.TryGetComponent(out MeshRenderer mesh))
        {
            mesh.material.color = color;
        }
    }
}
