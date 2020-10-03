using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool paused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (showingText) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("etext");
                textChainIndex++;
                UpdateTextPanel();
            }
        }
        timer += Time.deltaTime * timerMult;
        if (timer >= maxTimer) RevertAll();
        loopIndicator.fillAmount = 1 - (timer / maxTimer);
    }

    void SetPause(bool newValue)
    {
        if (newValue) {
            // pausing the game
            Time.timeScale = 0f;
            paused = true;
        }
        else {
            // unpausing the game
            Time.timeScale = 1f;
            paused = false;
        }
    }

    #region Text Panel
    
    public GameObject textPanel;
    public TextMeshProUGUI text;
    string[] currentTextChain;
    int textChainIndex = 0;
    bool showingText = false;

    public void ShowText(string text)
    {
        currentTextChain = new string[]{text};
        ShowText();
    }

    public void ShowTextChain(string[] texts)
    {
        currentTextChain = texts;
        ShowText();
    }

    void ShowText()
    {
        showingText = true;
        textChainIndex = 0;
        textPanel.SetActive(true);
        UpdateTextPanel();
        SetPause(true);
    }

    void UpdateTextPanel()
    {
        if (textChainIndex >= currentTextChain.Length) {
            textPanel.SetActive(false);
            showingText = false;
            SetPause(false);
            return;
        }
        text.text = currentTextChain[textChainIndex];
    }

    #endregion

    #region Level Management

    public Player player;
    public Transform[] prisonLevels;
    int level = 0;
    Color litLevelColor = new Color(0.2f,0.18f,0.18f);

    void ChangeLevel(int newLevel)
    {
        SpriteRenderer sr;
        for (int i = 0; i < 4; i++) {
            float alpha = i == newLevel ? 1f : 0f;
            foreach (Transform levelChild in prisonLevels[i]) {
                SetAlphaOfChildren(levelChild,alpha);
            }
            sr = prisonLevels[i].GetComponent<SpriteRenderer>();
            sr.color = alpha > 0.5f ? litLevelColor : Color.black;
        }
        level = newLevel;
        player.transform.position = Vector3.up * (2.52f + newLevel * 0.625f);
    }

    void SetAlphaOfChildren(Transform parent, float alpha)
    {
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();
        if (sr) sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,alpha);
        foreach (Transform child in parent) {
            sr = child.GetComponent<SpriteRenderer>();
            if (sr) sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,alpha);
            SetAlphaOfChildren(child,alpha);
        }
    }

    public void GoUp()
    {
        Debug.Log("up");
        ChangeLevel(level + 1);
    }

    public void GoDown()
    {
        Debug.Log("down");
        ChangeLevel(level - 1);
    }

    #endregion

    #region Timeloop stuff

    float timer = 0f;
    float timerMult = 1f;
    const float maxTimer = 5f;
    public Image loopIndicator;

    List<TimeloopAffected> timeloopAffecteds = new List<TimeloopAffected>();

    public void AddTimeloopAffected(TimeloopAffected ta)
    {
        timeloopAffecteds.Add(ta);
    }

    void RevertAll()
    {
        Debug.Log("reverting all");
        foreach (TimeloopAffected ta in timeloopAffecteds) ta.Revert();
        timer = 0f;
        ChangeLevel(0);
    }

    public void PlayerEnteredBubble()
    {
        timerMult = 0f;
    }

    public void PlayerLeftBubble()
    {
        timerMult = 1f;
    }

    #endregion

}
