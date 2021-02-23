using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameResources
{
    Bees,
    Honey,
    Prestige
}
public class ResourceTracker : MonoBehaviour
{
    public static float startingHoney = 50f, startingBees = 150f, startingPrestige = 50f;
    public static float honey = startingHoney;
    public static float bees = startingBees;
    public static float prestige = startingPrestige;
    public static int turnCounter;
    public static int decisionsMade = 0;

}
