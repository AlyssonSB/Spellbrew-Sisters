using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform handPoint;
    public GameObject heldItem;
   // public Animator animator;
    private Interactable bestInteractable;
    private void Start()
    {
       // animator = GetComponent<Animator>();

    }
    void Update()
    {
        InteractionUI.Instance.UpdateUI(this, bestInteractable);
        InteractionUI.Instance.dropButton.SetActive(heldItem != null);

        /* if (heldItem != null)
             animator.SetBool("Hold", true);
         else
             animator.SetBool("Hold", false);*/

        HighlightCurrent();

        if (Input.GetKeyDown(KeyCode.E))
            InteractButton();
        if (Input.GetKeyDown(KeyCode.G))
            DropItem();

    }

    public void InteractButton()
    {
        bestInteractable?.Interact(this);
    }

    void HighlightCurrent()
    {
        foreach (var i in FindObjectsOfType<Interactable>())
            i.Highlight(false);

        if (bestInteractable != null)
            bestInteractable.Highlight(true);
    }
    public void DropItem()
    {
        if (heldItem == null) return;

        heldItem.transform.SetParent(null);

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        Collider col = heldItem.GetComponent<BoxCollider>();
        if (col != null) col.enabled = true;

        heldItem.GetComponent<Ingredient>()?.StartDecay();

        heldItem = null;
        bestInteractable = null;


    }

    public void PickItem(GameObject item)
    {
        heldItem = item;

        item.transform.SetParent(handPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        Collider col = item.GetComponent<BoxCollider>();
        if (col != null) col.enabled = false;

        item.GetComponent<Ingredient>()?.StopDecay();

        bestInteractable = null;
    }

    private void OnTriggerStay(Collider other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i == null) return;

        float dist = Vector3.Distance(transform.position, i.transform.position);

        if (bestInteractable == null ||
            dist < Vector3.Distance(transform.position, bestInteractable.transform.position))
        {
            bestInteractable = i;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i == bestInteractable)
            bestInteractable = null;
    }
}