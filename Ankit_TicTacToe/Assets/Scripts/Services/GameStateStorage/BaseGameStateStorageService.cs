using UnityEngine;

public abstract class BaseGameStateStorageService : MonoBehaviour
{
    public abstract bool HasActiveState();
    public abstract string GetActiveGameState();
    public abstract void DeleteActiveGameState();
    public abstract void SaveActiveGameState(string gameState);
}