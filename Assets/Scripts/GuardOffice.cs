using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardOffice : Interactable
{
    public Door l0Door;
    public GameObject keycardPrefab;
    bool substituteCompelete = false;
    bool tookKeycard = false;
    string[] maskerPartNames = new string[]{"Bio-heater","Radio transmitter","Signal scrambler"};
    Dictionary<string,string> concreteToVague = new Dictionary<string, string>();
    List<string> neededMaskerParts = new List<string>();

    void Start()
    {
        BaseStart("Guard office");
        foreach (string n in maskerPartNames) neededMaskerParts.Add(n);
        concreteToVague.Add("Bio-heater","something to fake my bodyheat");
        concreteToVague.Add("Radio transmitter","something to transmit a signal");
        concreteToVague.Add("Signal scrambler","something to fake my suit's signal");
    }
    public override void Interact()
    {
        if (player.carrying && player.carrying.tag.Equals("presenceMaskerPart")) {
            neededMaskerParts.Remove(player.carrying.name);
            player.carrying.UseCarriable();
            if (neededMaskerParts.Count == 0) {
                gm.ShowTextChain(new string[]{
                    "Aaand done! This should fake my presence at the prisoner level.",
                    "Hopefully the lockdown alarms are down once I go out there again. Only one way to find out..."
                });
                substituteCompelete = true;
                gm.UpdateLockdown();
            }
            else {
                string t = "I still need ";
                for (int i = 0; i < neededMaskerParts.Count; i++) t += concreteToVague[neededMaskerParts[i]] + " and ";
                t = t.Substring(0,t.Length-5) + ".";
                gm.ShowText(t);
            }
        }
        else if (!player.carrying && !player.dragging && !tookKeycard) {
            GameObject go = Instantiate(keycardPrefab,Vector3.zero,Quaternion.identity);
            Carriable c = go.GetComponent<Carriable>();
            c.ForceSetup(player,gm);
            c.Interact(); // picks it up
            gm.ShowTextChain(new string[]{
                "The guards are dead!?",
                "Right, no time, no time. I'll take their keycard and worry about what's happening later."
            });
            tookKeycard = true;
        }
        else if (player.GetGameData("sawl2keypad") != null) {
            string t = "I still need ";
            for (int i = 0; i < neededMaskerParts.Count; i++) t += concreteToVague[neededMaskerParts[i]] + " and ";
            t = t.Substring(0,t.Length-5) + ".";
            gm.ShowText(t);
        }
        else if (tookKeycard) {
            gm.ShowTextChain(new string[]{
                "What a gruesome sight...",
                "...",
                "Ahem, right. Escape first, sadness second.",
                "Besides that keycard, the guard office doesn't have anything useful."
            });
        }
    }
    public override void ResetState()
    {
        substituteCompelete = false;
        tookKeycard = false;
        if (l0Door.hasBeenUnlocked) tookKeycard = true;
        neededMaskerParts.Clear();
        foreach (string n in maskerPartNames) {
            neededMaskerParts.Add(n);
            if (gm.usedItems.Contains(n)) gm.usedItems.Remove(n);
        }
    }

    public bool IsSubstituteComplete()
    {
        return substituteCompelete;
    }

    public override bool IsValidInteractable() {return true;}
}
