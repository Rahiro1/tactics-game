using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNotificationDisplay : MonoBehaviour
{
    public GameObject notificationPrefab;
    public List<SkillNotification> notifications;
    public WaitForSeconds waitForOne = new WaitForSeconds(1f);

    public void AddNotification(string notificationText)
    {
        /*foreach(SkillNotification notification in notifications)
        {
            if(notification.isActiveAndEnabled == false)
            {
                notification.OpenMenu(notificationText);
                return;
            }
        }

        UnloadAll();
        notifications[0].OpenMenu(notificationText);
        */

        StartCoroutine(SpawnAndDespawnNotification(notificationText));
    }

    public void UnloadAll()
    {
        foreach(SkillNotification notification in notifications)
        {
            notification.CloseMenu();
        } 
    }

    public IEnumerator SpawnAndDespawnNotification(string notificationText)
    {
        GameObject newNotification = Instantiate(notificationPrefab, transform);
        newNotification.GetComponent<SkillNotification>().OpenMenu(notificationText);


        yield return waitForOne;

        Destroy(newNotification);

        yield break;
    }
}
