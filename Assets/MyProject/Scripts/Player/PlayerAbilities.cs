using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities Instance;
    public bool elementalActive = true;
    public bool temporalActive = true;

    private Cauldron currentCauldron;

    void Update() 
    {
        if (currentCauldron == null) return;

        if (elementalActive)
        {
            if (Input.GetKeyDown(KeyCode.U))
                currentCauldron.AddHeat();

            if (Input.GetKeyDown(KeyCode.Y))
                currentCauldron.AddCold();
        }

        if (temporalActive)
        {
            if (Input.GetKeyDown(KeyCode.J))
                currentCauldron.AddSpeed();

            if (Input.GetKeyDown(KeyCode.H))
                currentCauldron.AddSlow();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        Cauldron c = other.GetComponent<Cauldron>();
        if (c != null)
            currentCauldron = c;
    }

    private void OnTriggerExit(Collider other)
    {
        Cauldron c = other.GetComponent<Cauldron>();
        if (c == currentCauldron)
            currentCauldron = null;
    }
}
    
