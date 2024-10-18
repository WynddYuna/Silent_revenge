using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private const string API_URL = "http://localhost/game_inventory_api/api.php";

    public List<Item> items = new List<Item>();
    public GameObject itemButtonPrefab; // Prefab for the item button
    public Transform contentPanel; // Panel where the item buttons will be added

    void Start()
    {
        StartCoroutine(GetItems());
    }

    private IEnumerator GetItems()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(API_URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                items = JsonUtility.FromJson<ItemList>(jsonResponse).items;
                UpdateInventoryUI();
            }
        }
    }

    [System.Serializable]
    private class ItemList
    {
        public List<Item> items;
    }

    private void UpdateInventoryUI()
    {
        // Clear existing items in the UI
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Populate the UI with items from the inventory
        foreach (Item item in items)
        {
            GameObject newItemButton = Instantiate(itemButtonPrefab, contentPanel);
            newItemButton.GetComponentInChildren<Text>().text = item.name; // Set item name
            newItemButton.GetComponent<Button>().onClick.AddListener(() => OnItemButtonClicked(item));
        }
    }

    private void OnItemButtonClicked(Item item)
    {
        Debug.Log("Item clicked: " + item.name);
        // Implement further interaction logic here (e.g., use, equip, drop)
    }
}