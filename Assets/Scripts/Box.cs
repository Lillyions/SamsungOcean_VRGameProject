using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    public bool isRight;
    public Material rightMaterial, leftMaterial;

    IEnumerator Start()
    {
        TryGetComponent (out Rigidbody rb);
        TryGetComponent (out MeshRenderer mesh);

        if (isRight) mesh.sharedMaterial = rightMaterial;
        else mesh.sharedMaterial = leftMaterial;

        rb.AddRelativeForce(Vector3.forward * 30, ForceMode.Impulse); 
        yield return new WaitForSeconds (0.5f);
        rb.AddRelativeForce(-Vector3.forward * 25, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Right" && isRight || other.tag == "Left" && !isRight)
            Destroy(gameObject);
    }
}
