using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class WindowBoundsCollider3D : MonoBehaviour
{
[Tooltip("Camera-relative size of the bounds (1 = full window, 0.5 = half). Useful for safe-areas")]
[SerializeField] float scale = 1f;

[Tooltip("A larger radius helps prevent fast-moving objects from clipping through")]
[SerializeField] float edgeRadius = 10f;
	new Camera camera;
    MeshCollider borderCollider;

void Start()
{
    CreateCollider();
}

void CreateCollider()
{
    camera = GetComponent<Camera>();
    borderCollider = gameObject.AddComponent<MeshCollider>();

    var cameraPlane = camera.orthographic ? 0 : -camera.transform.position.z;
    borderCollider.sharedMesh = CreateMeshFromEdges();

    var edgeOffset = new Vector3(edgeRadius, 0, 0);
    var meshVertices = borderCollider.sharedMesh.vertices;
    for (var i = 0; i < meshVertices.Length; i++)
    {
        var point = borderCollider.transform.TransformPoint(meshVertices[i]);
        point.z = cameraPlane;
        meshVertices[i] = borderCollider.transform.InverseTransformPoint(point) - edgeOffset;
    }
    borderCollider.sharedMesh.vertices = meshVertices;
}

Mesh CreateMeshFromEdges()
{
    var mesh = new Mesh();
    mesh.vertices = new[]
    {
        new Vector3(scale - 1, scale - 1, 100), //bottom left
        new Vector3(scale - 1, 1 - scale, 100), //top left
        new Vector3(1 - scale, 1 - scale, 100), //top right
        new Vector3(1 - scale, scale - 1, 100), //bottom right
        new Vector3(scale - 1, scale - 1, 100)  //close the loop
    };
    mesh.triangles = new[] { 0, 1, 2, 2, 3, 0 };
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
    return mesh;
}

void OnDrawGizmosSelected()
{
    var maxScale = scale;
    var minScale = 1f - scale;

    if (!camera)
    {
        camera = GetComponent<Camera>();
    }

    var cameraPlane = camera.orthographic ? 0 : -camera.transform.position.z;
    var pointA = camera.ViewportToWorldPoint(new Vector3(minScale, minScale, cameraPlane));
    var pointB = camera.ViewportToWorldPoint(new Vector3(minScale, maxScale, cameraPlane));
    var pointC = camera.ViewportToWorldPoint(new Vector3(maxScale, maxScale, cameraPlane));
    var pointD = camera.ViewportToWorldPoint(new Vector3(maxScale, minScale, cameraPlane));
    Gizmos.DrawCube(pointA, pointB);
    Gizmos.DrawCube(pointB, pointC);
    Gizmos.DrawCube(pointC, pointD);
    Gizmos.DrawCube(pointD, pointA);
}
}