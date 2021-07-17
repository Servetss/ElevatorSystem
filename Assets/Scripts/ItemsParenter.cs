using UnityEngine;

public class ItemsParenter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>())
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Movement>())
        {
            other.transform.SetParent(null);
        }
    }
}
