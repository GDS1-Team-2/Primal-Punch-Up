using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform portalPrefab;
    private Transform portalA;
    private Transform portalB;
    private bool hasPortal = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlacePortal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PortalPickup"))
        {
            hasPortal = true;
            Destroy(other.gameObject);
            Debug.Log("Portal picked up!");
        }
    }

    void PlacePortal()
    {
        if (hasPortal)
        {
            if (portalA == null)
            {
                portalA = Instantiate(portalPrefab, CalculatePlacementPosition(), Quaternion.identity);
                Debug.Log("First portal placed.");
            }
            else if (portalB == null)
            {
                portalB = Instantiate(portalPrefab, CalculatePlacementPosition(), Quaternion.identity);
                hasPortal = false;
                Debug.Log("Second portal placed.");
                EnableTeleportation();
            }
        }
    }

    Vector3 CalculatePlacementPosition()
    {
        return transform.position + transform.forward * 2;
    }

    void EnableTeleportation()
    {
        portalA.gameObject.tag = "Teleport";
        portalB.gameObject.tag = "Teleport";
        portalA.gameObject.AddComponent<Rigidbody>().isKinematic = true;
        portalA.gameObject.AddComponent<BoxCollider>().isTrigger = true;

        portalB.gameObject.AddComponent<Rigidbody>().isKinematic = true;
        portalB.gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            Transform destinationPortal = (other.transform == portalA) ? portalB : portalA;
            transform.position = destinationPortal.position + destinationPortal.forward * 1;
            Debug.Log("Teleported to " + destinationPortal.name);
        }
    }
}
