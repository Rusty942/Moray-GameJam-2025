using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class IngredientSelector : MonoBehaviour
{
    public GameObject[] xs;  // List of xs (used to activate associated x)
    public TextMeshPro[] ingredientTexts; // Ingredient text displays
    public GameObject[] fillObjects; // List of fill objects
    public GameObject[] recipes; // List of recipe objects (end products)
    public AudioSource buttNoise;
    public AudioSource winner;
    public GameObject menu;

    private string[] items = 
    { 
        "SNOW BERRY", "CRYSTAL LEMON", "FLAME CHILLI", "BOMB MUSHROOM", 
        "MIST ALMOND", "RUST TEALEAF", "STAR CHOCOLATE", "RAINBOW SYRUP" 
    };

    private int selectedIndex = 0; // Track current selection
    private List<int> selectedItems = new List<int>(); // List to track selected indices
    private float inputCooldown = 0.2f; // Delay between input presses
    private float lastInputTime = 0f; // Time of last input

    void Start()
    {
        menu.SetActive(true);

        if (StartData.ingredients == null) return;

        // Set all recipes to inactive at the start
        foreach (var recipe in recipes)
        {
            recipe.SetActive(false);
        }

        Dictionary<string, int> ingredientCounts = items.ToDictionary(
            item => item, 
            item => StartData.ingredients.Count(ing => ing == item)
        );

        for (int i = 0; i < items.Length; i++)
        {
            if (ingredientTexts[i]) ingredientTexts[i].text = ingredientCounts[items[i]].ToString();
        }

        UpdateSelection();
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"); // Joystick or Keyboard arrow keys

        if (Time.time - lastInputTime > inputCooldown) // Prevent rapid movement
        {
            if (verticalInput < -0.5f) // Move down
            {
                MoveSelection(1);
                lastInputTime = Time.time;
            }
            else if (verticalInput > 0.5f) // Move up
            {
                MoveSelection(-1);
                lastInputTime = Time.time;
            }
        }

        if (Input.GetButtonDown("Submit")) // Check if submit button (e.g., "Enter" or "A" on controller) is pressed
        {
            HandleSelection(selectedIndex); // Handle the action based on the selected index
        }
    }

    private void MoveSelection(int direction)
    {
        fillObjects[selectedIndex].SetActive(false); // Deactivate old selection

        selectedIndex += direction;

        if (selectedIndex < 0) selectedIndex = 0;
        if (selectedIndex >= fillObjects.Length) selectedIndex = fillObjects.Length - 1;

        fillObjects[selectedIndex].SetActive(true); // Activate new selection
        buttNoise.Play();
    }

    private void HandleSelection(int index)
    {
        // Check if the ingredient count is 0, disable selection if it is
        if (GetIngredientCount(index) == 0)
        {
            return; // Do nothing if ingredient count is 0
        }

        // If the item is already selected, deselect it
        if (selectedItems.Contains(index))
        {
            selectedItems.Remove(index);
            xs[index].SetActive(false); // Deactivate corresponding xs item
        }
        else
        {
            // If less than 2 items are selected, select this one
            if (selectedItems.Count < 2)
            {
                selectedItems.Add(index);
                xs[index].SetActive(true); // Activate corresponding xs item
            }
        }

        // If more than 2 items are selected, do nothing (you can also add feedback if needed)
        if (selectedItems.Count > 2)
        {
            selectedItems.RemoveAt(0); // Deselect the first item if there are more than 2 selected
            xs[selectedItems[0]].SetActive(false); // Deactivate corresponding xs item
        }

        // Check if the selected items match any valid combos
        CheckCombos();
        
        // Debug for visual feedback on selected items
        Debug.Log("Selected Items: " + string.Join(", ", selectedItems.Select(i => items[i])));
    }

    private void CheckCombos()
    {
        // Define the valid combos and their corresponding recipe indices
        Dictionary<string, int> validCombos = new Dictionary<string, int>
        {
            { "SNOW BERRY + STAR CHOCOLATE", 0 },
            { "CRYSTAL LEMON + FLAME CHILLI", 1 },
            { "CRYSTAL LEMON + MIST ALMOND", 2 },
            { "RAINBOW SYRUP + BOMB MUSHROOM", 3 },
            { "STAR CHOCOLATE + RUST TEALEAF", 4 },
            { "MIST ALMOND + FLAME CHILLI", 5 }
        };

        // If two items are selected, check if they form a valid combo
        if (selectedItems.Count == 2)
        {
            string combo = items[selectedItems[0]] + " + " + items[selectedItems[1]];

            if (validCombos.ContainsKey(combo))
            {
                // Set all recipes to inactive first
                foreach (var recipe in recipes)
                {
                    recipe.SetActive(false);
                }

                // Activate the recipe corresponding to the valid combo
                int recipeIndex = validCombos[combo];
                recipes[recipeIndex].SetActive(true); // Activate the specific recipe

                // ADD YOUR COMBO ACTIONS HERE
                StartData.currencyAmm += 100;
                menu.SetActive(false); // Example of menu being deactivated
                Debug.Log("Combo selected: " + combo);
                winner.Play();
            }
        }
    }

    private int GetIngredientCount(int index)
    {
        // Get the ingredient count for the selected item
        string ingredient = items[index];
        return StartData.ingredients.Count(ing => ing == ingredient);
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < fillObjects.Length; i++)
        {
            fillObjects[i].SetActive(i == selectedIndex);
        }
    }
}
