using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject normal, choice, unChoiceable;
    [SerializeField] private bool isActive = true;

    void Start()
    {
        if (!isActive)
        {
            normal.SetActive(false);
            choice.SetActive(false);
            unChoiceable.SetActive(true);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive)
        {
            normal.SetActive(false);
            choice.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isActive)
        {
            choice.SetActive(false);
            normal.SetActive(true);
        }
    }

    public void Continue()
    {
        if (isActive)
        {
            transform.parent.gameObject.SetActive(false);
            Time.timeScale = 1f;
            choice.SetActive(false);
            normal.SetActive(true);
        }
    }

    public void NewGame()
    {
        if (isActive)
        {
            Debug.Log("Error");
        }
    }

    public void Retry()
    {
        if (isActive)
        {
            Debug.Log("Error");
        }
    }

    public void Option()
    {
        if (isActive)
        {
            Debug.Log("Error");
        }
    }
}
