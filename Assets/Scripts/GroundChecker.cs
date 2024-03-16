using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerFinder.GetIndex(Layer.Ground))
        {
            Debug.Log("grounded");
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerFinder.GetIndex(Layer.Ground))
        {
            Debug.Log("not grounded");
            isGrounded = false;
        }
    }
}
