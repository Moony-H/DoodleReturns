using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStartSetting : MonoBehaviour
{
    public static StageStartSetting instance;

    [Range(0, 20)]
    public int firstInkCount;
    public Transform playerFirstPosition;
    public GameObject goalDoor;
    public string nextStage;
    public List<Item> canUseItemList;

    public GameObject player;
    public ItemSystem itemSys;

    private void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
        player.SetActive(false);
        itemSys = GameObject.Find("ItemUI").GetComponent<ItemSystem>();
        goalDoor.AddComponent<StageStartSetting_GoalDoor>();
        itemSys.CanUseItem(canUseItemList);
    }

    private void Start()
    {
        PlayerReset();
    }

    public void PlayerReset()
    {
        player.gameObject.transform.position = playerFirstPosition.position;
        player.SetActive(true);
        itemSys.ItemReset();
        itemSys.ItemGet(Item.ink, firstInkCount);
    }

    public void NextStage()
    {
        DefaultUI.instance.OpenScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
