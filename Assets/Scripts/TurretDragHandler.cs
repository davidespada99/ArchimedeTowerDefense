using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurretDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private GameObject turretPrefab;

    [Header("Attributes")]
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private Color invalidColor = Color.red;

    [SerializeField] private int cost = 25;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    private GameObject dragImage;

    void Update(){
        Image image = gameObject.GetComponent<Image>();
        Color currentColor = image.color;
        if(LevelManager.main.currency < cost){
            currentColor.a = 0.5f;
            image.color = currentColor;
        }
        else{
            currentColor.a = 1f;
            image.color = currentColor;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
         if(LevelManager.main.currency < cost){
            return;
        }
        // Create a new Image GameObject for dragging
        dragImage = new GameObject("DragImage", typeof(Image), typeof(CanvasGroup));
        dragImage.transform.SetParent(canvas.transform, false);

        // Copy the sprite from the original Image
        Image originalImage = GetComponent<Image>();
        Image dragImageComponent = dragImage.GetComponent<Image>();
        dragImageComponent.sprite = originalImage.sprite;

        // Match the size of the original image
        dragImageComponent.rectTransform.sizeDelta = originalImage.rectTransform.sizeDelta;

        // Set the drag image properties
        CanvasGroup dragCanvasGroup = dragImage.GetComponent<CanvasGroup>();
        dragCanvasGroup.alpha = 0.6f;
        dragCanvasGroup.blocksRaycasts = false;

        // Set the initial position of the drag image
        dragImage.transform.position = originalImage.transform.position;

        canvasGroup.blocksRaycasts = true; // Keep the original icon interactable
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragImage != null)
        {
            RectTransform dragRectTransform = dragImage.GetComponent<RectTransform>();
            dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            // Check if the current position is valid
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool validPosition = IsValidPlacement(worldPosition);
            // Change the color based on validity
            dragImage.GetComponent<Image>().color = validPosition ? validColor : invalidColor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragImage != null)
        {
            // Convert screen point to world point
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsValidPlacement(worldPosition))
            {
                PlaceTurret(worldPosition);
                LevelManager.main.SpendCurrency(cost);
            }

            // Destroy the drag image
            Destroy(dragImage);
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private bool IsValidPlacement(Vector2 position)
    {
        // Check if the position is over the Shop UI element
        if (IsPointerOverShopUIElement())
        {
            return false;
        }

        // Check if the position is over the path or another turret
        Collider2D hitCollider = Physics2D.OverlapPoint(position);
        if (hitCollider != null)
        {
            if (hitCollider.CompareTag("Path") || hitCollider.CompareTag("Turret"))
            {
                return false;
            }
        }

        // Check if the position is too close to another turret
        float minDistanceBetweenTurrets = 1.3f; // Adjust as necessary
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistanceBetweenTurrets);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Turret"))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsPointerOverShopUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Shop"))
            {
                return true;
            }
        }

        return false;
    }

    private void PlaceTurret(Vector2 position)
    {
        Instantiate(turretPrefab, position, Quaternion.identity);
    }
}
