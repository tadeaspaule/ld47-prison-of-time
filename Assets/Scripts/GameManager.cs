﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool paused = false;
    public bool blockInput = false;
    public bool lockdown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeLevel(1);
        playerTA = player.GetComponent<TimeloopAffected>();
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
        if (Input.GetKeyDown(KeyCode.Q)) {
            lockdown = !lockdown;
            foreach (Transform level in prisonLevels) level.GetComponent<PrisonLevel>().ToggleLockdown(lockdown);
        }
        loopIndicator.fillAmount = 1 - (playerTA.timer / TimeloopAffected.maxTimer);
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
        StartCoroutine(DelayedSetShowingText());
        textChainIndex = 0;
        textPanel.SetActive(true);
        UpdateTextPanel();
        SetPause(true);
    }

    IEnumerator DelayedShowText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowText(text);
    }

    IEnumerator DelayedSetShowingText()
    {
        yield return new WaitForEndOfFrame();
        showingText = true;
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
    public GameObject[] levelHiders;
    int level = 0;
    Color litLevelColor = new Color(0.2f,0.18f,0.18f);
    public ScavangeScreen scavangeScreen;
    public GuardOffice guardOffice;

    void ChangeLevel(int newLevel)
    {
        level = newLevel;
        for (int i = 0; i < 4; i++) {
            levelHiders[i].SetActive(i != newLevel);
        }
        foreach (Transform bubble in extraBubbleHolder) {
            // hide bubbles on other levels so you don't see through surfaces
            bool bubbleEnabled = bubble.gameObject.name.StartsWith($"l{level}");
            bubble.GetComponent<Bubble>().ToggleVisuals(bubbleEnabled);
        }
        player.transform.position = Vector3.up * (2.52f + newLevel * 0.625f);
        UpdateLockdown();
    }

    public void UpdateLockdown()
    {
        lockdown = level != 0 && !guardOffice.IsSubstituteComplete();
        foreach (Transform level in prisonLevels) level.GetComponent<PrisonLevel>().ToggleLockdown(lockdown);
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

    public Image loopIndicator;
    TimeloopAffected playerTA;
    public GameObject bubblePrefab;
    public Transform extraBubbleHolder;

    List<TimeloopAffected> timeloopAffecteds = new List<TimeloopAffected>();

    public void AddTimeloopAffected(TimeloopAffected ta)
    {
        timeloopAffecteds.Add(ta);
    }

    public void RevertPlayer()
    {
        playerTA.Revert();
        player.WipeGameData(); // -> the character forgets, though the player doesn't? idk if comment or leave
        player.prisonHolder.rotation = Quaternion.identity;
        player.bubbleTools.Clear();
        ChangeLevel(0);
    }

    public void PlaceBubble(Vector3 position)
    {
        GameObject bubble = Instantiate(bubblePrefab,position,Quaternion.identity,extraBubbleHolder);
        bubble.name = $"l{level}bubble";
        if (player.GetGameData("placedfirstbubble") == null) StartCoroutine(DelayedShowText("Yikes, that bubble looks unstable. Hopefully it doesn't break on me.",0.6f));
        player.UpdateGameData("placedfirstbubble","true");
    }

    #endregion

}
