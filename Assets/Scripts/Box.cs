using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    public bool isRight;
    public HitArea hitArea;
    public Material rightMaterial, leftMaterial;

    IEnumerator Start()
    {
        TryGetComponent(out Rigidbody rb);
        TryGetComponent(out MeshRenderer mesh);
        hitArea = GetComponentInChildren<HitArea>();
        hitArea.OneHit += Hit;

        if (isRight) mesh.sharedMaterial = rightMaterial;
        else mesh.sharedMaterial = leftMaterial;

        rb.AddRelativeForce(Vector3.forward * 30, ForceMode.Impulse);
        //yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;
        while (elapsed < 0.5)
        {
            transform.Rotate(0, 0, 360f * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        int random = Random.Range(0, 4);
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = random * 90f;
        transform.eulerAngles = currentRotation;

        rb.AddRelativeForce(-Vector3.forward * 25, ForceMode.VelocityChange);
    }

    void OnDisable()
    {
        hitArea.OneHit -= Hit;
    }

    void OnTriggerEnter(Collider saber)
    {
        //Destroy(hitArea.gameObject);
        hitArea.OneHit -= Hit;
    }

    void Hit(Collider saber)
    {
        if (saber.tag == "Right" && isRight ||
            saber.tag == "Left" && !isRight)
            Destroy(gameObject);
    }
}
