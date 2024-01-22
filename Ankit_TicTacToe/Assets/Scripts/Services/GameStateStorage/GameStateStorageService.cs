using UnityEngine;

public class GameStateStorageService : BaseGameStateStorageService
{
    private const string StorageKey = "ActiveGameState";
    
    public override bool HasActiveState()
    {
        return PlayerPrefs.HasKey(StorageKey);
    }
    
    public override string GetActiveGameState()
    {
        return PlayerPrefs.GetString(StorageKey);
    }

    public override void DeleteActiveGameState()
    {
        PlayerPrefs.DeleteKey(StorageKey);
    }

    public override void SaveActiveGameState(string gameState)
    {
        PlayerPrefs.SetString(StorageKey, gameState);
    }
}
