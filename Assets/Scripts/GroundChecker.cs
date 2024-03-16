using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerFinder.GetIndex(Layer.Ground))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerFinder.GetIndex(Layer.Ground))
        {
            isGrounded = false;
        }
    }
}
