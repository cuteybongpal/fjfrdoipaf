using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string MoveText;
    public Define.Scenes SceneNum;
    public GameObject UI_MoveSceneConfirm;
    public GameObject RespawnPos;
    private void Start()
    {
        
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        UI_ConfirmSceneChange ui = Instantiate(UI_MoveSceneConfirm).GetComponent<UI_ConfirmSceneChange>();
        ui.Init(MoveText, SceneNum);
        if (RespawnPos != null)
            GameManager.Instance.ReSpawnPos[GameManager.Instance.CurrentStage] = RespawnPos.transform.position;
    }
}
