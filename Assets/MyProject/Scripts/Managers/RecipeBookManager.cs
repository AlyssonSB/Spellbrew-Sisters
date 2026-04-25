using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class RecipeBookManager : MonoBehaviour
    {
        public static RecipeBookManager Instance;
        public GameObject recipeBookUI;
        public Transform recipesUIParent;
        public GameObject recipeUIPrefab;

        public List<PotionData> possiblePotions;
        // Use this for initialization
        void Start()
        { 
            foreach (PotionData potion in possiblePotions)
            {
                GameObject obj = Instantiate(recipeUIPrefab, recipesUIParent);

                RecipeUI ui = obj.GetComponent<RecipeUI>();

                ui.Setup(potion);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))  
                ShowBook();
        }

        public void ShowBook()
        {
            if (recipeBookUI.activeSelf)
                recipeBookUI.SetActive(false);
            else
                recipeBookUI.SetActive(true);
        }
    }
