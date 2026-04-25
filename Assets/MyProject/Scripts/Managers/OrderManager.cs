using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public List<PotionData> possiblePotions;

    public Transform ordersUIParent;
    public GameObject orderUIPrefab;

    private List<OrderUI> currentOrders = new List<OrderUI>();

    public int maxOrders = 8;
    public float orderInterval = 5f;

    public int deliveredCount = 0;
    public int failedCount = 0;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InvokeRepeating(nameof(GenerateOrder), 2f, orderInterval);
    }

    void GenerateOrder()
    {
        if (currentOrders.Count >= maxOrders) return;

        PotionData randomPotion = possiblePotions[Random.Range(0, possiblePotions.Count)];

        GameObject obj = Instantiate(orderUIPrefab, ordersUIParent);

        OrderUI ui = obj.GetComponent<OrderUI>();
        ui.Setup(randomPotion, this);

        currentOrders.Add(ui);
    }

    public bool TryCompleteOrder(PotionData deliveredPotion)
    {
        foreach (var order in currentOrders)
        {
            if (order.potion == deliveredPotion)
            {
                deliveredCount++;
                float gain = order.GetRatingValue();

                RatingManager.Instance.AddRating(gain);

                Debug.Log("Pedido entregue! Total: " + deliveredCount);

                RemoveOrder(order);
                return true;
            }
        }

        Debug.Log("Pedido não corresponde!");
        return false;
    }

    public void FailOrder(OrderUI order)
    {
        Debug.Log("Pedido expirou!");

        failedCount++;
        RatingManager.Instance.AddRating(-1f);

        RemoveOrder(order);
    }

    void RemoveOrder(OrderUI order)
    {
        currentOrders.Remove(order);
        Destroy(order.gameObject);
    }
}