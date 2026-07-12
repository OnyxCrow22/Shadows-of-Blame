using UnityEngine;

public interface IEvidence
{
    bool isReading { get; set; } // Cruical for checking if the player is reading the evidence or not.

    void PickUp(); // Handles the picking up functionality
    void CloseWindow(); // Handles closing the UI window
}
