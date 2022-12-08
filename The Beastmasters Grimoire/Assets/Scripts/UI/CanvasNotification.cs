/*
AUTHOR DD/MM/YY: Quentin 8/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CanvasNotification : MonoBehaviour
{
    Queue<GameObject> notifQueue = new Queue<GameObject>();
    int delay = 5;
    int fade = 2;
    bool clear = true;

    [HideInInspector] public UnityEvent stageCompleted;
    public GameObject questNotif;

    private void Awake()
    {
        EventManager.Instance.AddListener<NotificationEvent>(NewNotif);
    }

    private void NewNotif(NotificationEvent eventInfo)
    {
        TMP_Text[] itemText;
        GameObject newNotif = Instantiate(questNotif, this.transform, false);
        newNotif.SetActive(false);
        itemText = newNotif.GetComponentsInChildren<TMP_Text>();
        itemText[0].text = eventInfo.message;
        itemText[1].text = eventInfo.message2;

        notifQueue.Enqueue(newNotif);
    }

    private void Update()
    {
        if (clear && notifQueue.Count > 0)
        {
            clear = false;
            StartCoroutine(DisplayCoroutine());
        }
    }

    IEnumerator DisplayCoroutine()
    {
        while(notifQueue.Count > 0)
        {
            GameObject notif = notifQueue.Peek();

            notif.SetActive(true);
            StartCoroutine(FadeIn(notif));
            yield return new WaitForSeconds(delay);

            StartCoroutine(FadeOut(notif));
            yield return new WaitForSeconds(delay);

            Destroy(notif);
            notifQueue.Dequeue();
        }

        notifQueue.Clear();
        clear = true;
    }

    private IEnumerator FadeIn(GameObject notif)
    {
        float elapsed = 0;
        while(elapsed < fade)
        {
            elapsed += Time.deltaTime;
            notif.GetComponent<CanvasGroup>().alpha = elapsed/fade;
            yield return null;
        }
    }

    private IEnumerator FadeOut(GameObject notif)
    {
        float elapsed = 0;
        while (elapsed < fade)
        {
            elapsed += Time.deltaTime;
            notif.GetComponent<CanvasGroup>().alpha = 1 - elapsed / fade;
            yield return null;
        }
    }
}
