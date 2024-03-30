using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Carson Moon

public class Task2 : MonoBehaviour
{
    [Header("Name Array")]
    [SerializeField] string[] names;

    [Header("Name Hashsets")]
    [SerializeField] HashSet<string> nameSet = new HashSet<string>();

    [Header("Duplicate List")]
    [SerializeField] HashSet<string> duplicateNames = new HashSet<string>();


    private void Start() {
        // Create random name array.
        string[] randomNames = new string[15];

        for(int i=0; i<15; i++){
            int index = Random.Range(0, names.Length);

            randomNames[i] = names[index];
        }

        // Print name array.
        Debug.Log("Created the name array: " + string.Join(", ", randomNames));

        // Try to add random names to hashset to detect duplicates.
        Debug.Log("Duplicates:");
        for(int i=0; i<15; i++){
            if(!nameSet.Add(randomNames[i])){
                duplicateNames.Add(randomNames[i]);
            }
        }

        // Print out all of our duplicates.
        Debug.Log("The array has duplicate names: " + string.Join(", ", duplicateNames));
    }
}
