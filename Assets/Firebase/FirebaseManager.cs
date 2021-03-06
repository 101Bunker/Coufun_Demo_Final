using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Messaging;

public class FirebaseManager : MonoBehaviour
{
    FirebaseApp _app;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                FirebaseMessaging.TokenReceived += OnTokenReceived;
                FirebaseMessaging.MessageReceived += OnMessageReceived;
            }
            else
            {
                Debug.LogError("Firebase could not resolve all dependencies:" + task.Result);
            }
        });
    }


    void OnTokenReceived(object sender, TokenReceivedEventArgs e)
    {
        if (e != null)
        {
            Debug.LogFormat("Firebase token: {0}", e.Token);
        }
    }

    void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        if (e != null && e.Message != null && e.Message.Notification != null)
        {
            Debug.LogFormat("Firebase from: {0}, Title: {1}, Text: {2}",
                e.Message.From,
                e.Message.Notification.Title,
                e.Message.Notification.Body);
        }
    }
}
