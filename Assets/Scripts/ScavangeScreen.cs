using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScavangeScreen : MonoBehaviour
{
    public Player player;
    public GameManager gm;
    public GameObject scrapCarriablePrefab;
    public Image itemPreview;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    List<ScrapItem> items = new List<ScrapItem>();
    int itemIndex;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            itemIndex = (itemIndex+1) % items.Count;
            UpdateItemDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            itemIndex = (itemIndex-1) % items.Count;
            UpdateItemDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            GameObject go = Instantiate(scrapCarriablePrefab,Vector3.zero,Quaternion.identity);
            ScrapCarriable sc = go.GetComponent<ScrapCarriable>();
            sc.SetupSC(gm,player,items[itemIndex]);
            sc.Interact();
            gameObject.SetActive(false);
            gm.blockInput = false;
            items.RemoveAt(itemIndex);
        }
    }

    void UpdateItemDisplay()
    {
        itemPreview.sprite = items[itemIndex].GetSprite();
        itemName.text = items[itemIndex].name;
        itemDescription.text = items[itemIndex].description;
    }

    public void OpenScreen(List<ScrapItem> inItems)
    {
        gm.blockInput = true;
        gameObject.SetActive(true);
        items.Clear();
        foreach (ScrapItem item in inItems) items.Add(item);
        itemIndex = 0;
        UpdateItemDisplay();
    }
}
