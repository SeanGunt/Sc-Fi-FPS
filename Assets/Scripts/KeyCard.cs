using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public GameObject keyCard;

    public void Remove()
    {
        keyCard.SetActive(false);
    }
}
