using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{// reference of all player stats affected
    public GameConstants gameConstants;
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;
    private Dictionary<KeyCode, int> keyCodePowerupMap = new Dictionary<KeyCode, int>();

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
        keyCodePowerupMap.Add(KeyCode.Z, 0);
        keyCodePowerupMap.Add(KeyCode.W, 1);
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

    void RemovePowerupUI(int index)
    {
        powerupIcons[index].SetActive(false);
    }

    public void UsePowerup(KeyCode k)
    {
        int index = keyCodePowerupMap[k];
        Powerup p = powerupInventory.Get(index);
        if (p != null)
        {
            powerupInventory.Remove(index);
            RemovePowerupUI(index);
            marioMaxSpeed.ApplyChange(p.aboluteSpeedBooster);
            marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
            StartCoroutine(removeEffect(p));
        }
    }

    IEnumerator removeEffect(Powerup p)
    {
        yield return new WaitForSeconds(p.duration);
        marioMaxSpeed.ApplyChange(-p.aboluteSpeedBooster);
        marioJumpSpeed.ApplyChange(-p.absoluteJumpBooster);
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        for (int i = 0; i < powerupInventory.Items.Count; i++)
        {
            Powerup p = powerupInventory.Get(i);
            if (p != null)
            {
                powerupInventory.Remove(i);
            }
        }
    }
}