using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utiles
{
    public static string ReplaceCharWithString(string originalString, char charToReplace, string stringToReplaceWith)
    {
        // Split the original string into an array of characters
        char[] chars = originalString.ToCharArray();

        // Loop through each character in the array
        for (int i = 0; i < chars.Length; i++)
        {
            // If the character matches the one we want to replace
            if (chars[i] == charToReplace)
            {
                // Remove the character and insert the new string
                originalString = originalString.Remove(i, 1).Insert(i, stringToReplaceWith);
                i += stringToReplaceWith.Length - 1; // Adjust index for the added length
            }
        }
        return originalString;
    }


}
