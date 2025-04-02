using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public GameObject Puzzle;
    UI_Maze puzzle;
    private void Start()
    {
        if (GameManager.Instance.isPuzzleCleared[GameManager.Instance.CurrentStage])
            Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (puzzle != null)
            return;
        if (GameManager.Instance.isPuzzleCleared[GameManager.Instance.CurrentStage])
            return;

        puzzle = Instantiate(Puzzle).GetComponent<UI_Maze>();
        puzzle.Initialize(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (puzzle == null)
            return;
        if (GameManager.Instance.isPuzzleCleared[GameManager.Instance.CurrentStage])
            return;
        puzzle.Hide();
    }
    public void Clear()
    {
        GameManager.Instance.isPuzzleCleared[GameManager.Instance.CurrentStage] = true;
        StartCoroutine(CageDown());
    }
    IEnumerator CageDown()
    {
        Vector3 origin = transform.position;
        Vector3 targetPos = transform.position + Vector3.down * 10;
        float elapsedTime = 0;
        float duration = 5;
        while (elapsedTime < duration)
        {
            Vector3 pos = Vector3.Lerp(origin, targetPos, elapsedTime /duration);
            transform.position = pos;

            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
    
}
