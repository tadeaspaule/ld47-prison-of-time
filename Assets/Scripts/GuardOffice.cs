using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardOffice : Interactable
{
    public GameObject keycardPrefab;
    int presenceMaskerPartsAdded = 0;
    public static int maxMaskerParts = 3;
    bool substituteCompelete = false;
    bool tookKeycard = false;
    string[] maskerPartNames = new string[]{"MASKERPART1","MASKERPART2","MASKERPART3"};
    List<string> neededMaskerParts = new List<string>();

    void Start()
    {
        BaseStart();
        foreach (string n in maskerPartNames) neededMaskerParts.Add(n);
    }
    public override void Interact()
    {
        if (player.carrying && player.carrying.tag.Equals("presenceMaskerPart")) {
            presenceMaskerPartsAdded++;
            neededMaskerParts.Remove(player.carrying.name);
            player.carrying.UseCarriable();
            if (presenceMaskerPartsAdded == maxMaskerParts) {
                gm.ShowText("great masker is complete");
            substituteCompelete = true;
            gm.UpdateLockdown();
            }
            else {
                gm.ShowText($"I still need {string.Join(" and ",neededMaskerParts)}");
            }
        }
        else if (!player.carrying && !player.dragging && !tookKeycard) {
            GameObject go = Instantiate(keycardPrefab,Vector3.zero,Quaternion.identity);
            Carriable c = go.GetComponent<Carriable>();
            c.ForceSetup(player,gm);
            c.Interact();
            tookKeycard = true;
        }
    }
    public override void ResetState()
    {
        player.dragging = null;
        presenceMaskerPartsAdded = 0;
        substituteCompelete = false;
        tookKeycard = false;
        neededMaskerParts.Clear();
        foreach (string n in maskerPartNames) neededMaskerParts.Add(n);
    }

    public bool IsSubstituteComplete()
    {
        return substituteCompelete;
    }

    public override bool IsValidInteractable() {return true;}
}
