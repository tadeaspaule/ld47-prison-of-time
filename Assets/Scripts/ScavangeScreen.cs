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
    bool ignoreNextE = false; // otherwise goes into conflict when E'ing useless item and trying to dismiss the text with e

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            itemIndex = (itemIndex+1) % items.Count;
            ignoreNextE = false;
            UpdateItemDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            itemIndex--;
            while (itemIndex < 0) itemIndex += items.Count;
            ignoreNextE = false;
            UpdateItemDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            gameObject.SetActive(false);
            gm.blockInput = false;
            ignoreNextE = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !ignoreNextE) {
            if (items[itemIndex].tag != null && items[itemIndex].tag.Length > 1) {
                GameObject go = Instantiate(scrapCarriablePrefab,Vector3.zero,Quaternion.identity);
                go.name = items[itemIndex].name;
                go.tag = items[itemIndex].tag;
                ScrapCarriable sc = go.GetComponent<ScrapCarriable>();
                sc.SetupSC(gm,player,items[itemIndex]);
                sc.Interact();
                gameObject.SetActive(false);
                gm.blockInput = false;
                items.RemoveAt(itemIndex);
                ignoreNextE = false;
            }
            else {
                gm.ShowText("I don't think this will be helpful");
                ignoreNextE = true;
            }
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
        items = inItems;
        itemIndex = 0;
        UpdateItemDisplay();
    }
}
