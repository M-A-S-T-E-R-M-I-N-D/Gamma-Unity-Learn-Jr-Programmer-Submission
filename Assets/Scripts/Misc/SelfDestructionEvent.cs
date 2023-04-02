using UnityEngine;

public class SelfDestructionEvent : MonoBehaviour
{
    [SerializeField] private Transform objectToDestroy;

    public void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyObjectParent()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}