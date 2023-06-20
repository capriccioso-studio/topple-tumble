using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirestoreFunctions : MonoBehaviour
{
    Firebase.FirebaseApp App;
    FirebaseFirestore db;
    public bool isReady = false;
    // Start is called before the first frame update
    public void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            App = Firebase.FirebaseApp.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;
            isReady = true;
            // Set a flag here to indicate whether Firebase is ready to use by your app.
        } else {
            UnityEngine.Debug.LogError(System.String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }


    public void GetUser(string username, Action<UserClass> callback)
    {
        UserClass user = null;
        db.Collection("users").Document(username).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                user = snapshot.ConvertTo<UserClass>();
                Global.backend.username = user.Username;
                Global.backend.tokenAmount = user.Score;
                Global.backend.authText.text = user.Username;
                Global.backend.amountText.text = user.Score.ToString();
            }
            callback.Invoke(user);
        });
    }
    public void CreateUser(UserClass user)
    {
        Debug.Log("Creating user: " + user.Username);
        db.Collection("users").Document(user.Username).SetAsync(user);
        Debug.Log("Created user: " + user.UserId);
    }

    public void UpdateScore(string username, int score, Action callback = null)
    {
        Debug.Log("Updating score");
        db.Collection("users").Document(username).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                UserClass user = snapshot.ConvertTo<UserClass>();
                user.Score += score;
                
                db.Collection("users").Document(user.Username).SetAsync(user).ContinueWithOnMainThread(task =>
                {
                    callback?.Invoke();
                });
            }
        });
    }
    public void UpdateDrops(string username, int drops, Action callback = null)
    {
        db.Collection("users").Document(username).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                UserClass user = snapshot.ConvertTo<UserClass>();
                user.Drops += drops;
                db.Collection("users").Document(user.Username).SetAsync(user).ContinueWithOnMainThread(task =>
                {
                    callback?.Invoke();
                });
            }
        });
    }


    public void SubtractScore(string username, int score)
    {
        Debug.Log("Subtracting Score");
        db.Collection("users").Document(username).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                UserClass user = snapshot.ConvertTo<UserClass>();
                user.Score -= score;
                db.Collection("users").Document(user.Username).SetAsync(user);
            }
        });
    }

    public void AddScore(string username)
    {
        db.Collection("users").Document(username).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                UserClass user = snapshot.ConvertTo<UserClass>();
                user.Score += 1;
                db.Collection("users").Document(user.Username).SetAsync(user);
            }
        });
    }

}
