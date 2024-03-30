using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Carson Moon

public class Task1 : MonoBehaviour
{
    [Header("Username Attributes")]
    [SerializeField] string[] names;
    [SerializeField] char[] letters;

    [Header("Login Queue")]
    [SerializeField] int numLogins;
    [SerializeField] float loginCooldown;
    [SerializeField] Queue<string> loginQueue = new Queue<string>();


// Create our loging queue and start the corouting loop.
    private void Awake() {
        for(int i=0; i<numLogins; i++){
            AddRandomNameToQueue();
        }

        // Print initial login queue.
        Debug.LogFormat("Initial login queue created. There are {0} players in the queue: " + string.Join(", ", loginQueue), numLogins.ToString());

        StartCoroutine(TryLogin());
    }

// Grab a random name and add it to the queue.
    private void AddRandomNameToQueue(){
        string username = names[UnityEngine.Random.Range(0, names.Length)]
            + " " + letters[UnityEngine.Random.Range(0, letters.Length)];

        loginQueue.Enqueue(username);
    }

// Repeat until each player is logged in.
    IEnumerator TryLogin(){
        // Add another random person to the queue.
        AddRandomNameToQueue();

        // Login our top queue.
        string currentLogin = loginQueue.Dequeue();
        Debug.Log(currentLogin + " is now inside the game.");

        // Tease the next login.
        string nextLogin = loginQueue.Peek();
        Debug.Log(nextLogin + " is trying to login and added to the login queue.");

        // Wait for just a sec so its not spewing into the console.
        yield return new WaitForSeconds(loginCooldown);

        // Login the next person.
        StartCoroutine(TryLogin());
    }

}
