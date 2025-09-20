using UnityEngine;
 
public class DoorOpener : MonoBehaviour
{
    public GameObject door;      // Assigner la porte dans l’Inspector
    private Animator doorAnimator;
 
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("CloseDoor");
        }
    }
}