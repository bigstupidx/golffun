using UnityEngine;
using System.Collections;

public class PowerUpGUIHandler : MonoBehaviour {
    public bool isLocked = true;
    public string lockStatus = "";
    public int itemCost;
    public GameObject itemObject;
    public UIButton buttonOk;
    public string itemType;
	public GameObject characterPic;
	public int index;
	// Use this for initialization
	void Start () {
        lockStatus = LockStatus;
        if (bool.Parse(LockStatus))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<UILabel>().text =  itemCost.ToString();
        }
        else {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }

	}
	

    public string LockStatus
    {
        get
        {
            if (!PlayerPrefs.HasKey(name + "_lockStatus"))
            {
                PlayerPrefs.SetString(name + "_lockStatus", isLocked.ToString().ToLower());
                if (isLocked != bool.Parse(PlayerPrefs.GetString(name + "_lockStatus")))
                {

                    PlayerPrefs.SetString(name + "_lockStatus", isLocked.ToString().ToLower());
                }
            }
           

            lockStatus = PlayerPrefs.GetString(name + "_lockStatus");
            isLocked = bool.Parse(lockStatus);
            return lockStatus;
        }
        set
        {
            isLocked = bool.Parse(value);
            lockStatus = value;
            PlayerPrefs.SetString(name + "_lockStatus", value);
        }
    }

    public void  OnClick(){
        if (isLocked == true)
        {
            if ((TheGameManager.Instance.Coins / itemCost) >= 1f)
            {
                TheGameManager.Instance.Coins -= itemCost;
                transform.GetChild(1).gameObject.SetActive(false);
                LockStatus = "false";
                TheGameManager.Instance.itemPowerUpObject = itemObject;
                if (buttonOk!=null) {
                    buttonOk.gameObject.SetActive(true);
                }
                
            }
        }
        else {
                TheGameManager.Instance.itemPowerUpObject = itemObject;
                if (buttonOk != null)
                {
                    buttonOk.gameObject.SetActive(true);
                }
        }
    }



}
