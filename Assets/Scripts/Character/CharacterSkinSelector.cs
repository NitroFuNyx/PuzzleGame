using UnityEngine;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Zenject;

public class CharacterSkinSelector : MonoBehaviour
{
    private CurrentGameManager _currentGameManager;

    private Skin characterSkin;
    private SkeletonMecanim SkeletonMecanim;

    private Dictionary<CharacterTypes, string> charactersSkinsDictionary = new Dictionary<CharacterTypes, string>();

    private void Awake()
    {
        SkeletonMecanim = GetComponent<SkeletonMecanim>();
        FillSkinsDictionary();
    }

    private void Start()
    {
        _currentGameManager.OnCharacterChanged += CharacterChanged_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _currentGameManager.OnCharacterChanged -= CharacterChanged_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager)
    {
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    public void SetSkin(CharacterTypes character)
    {
        var skeleton = SkeletonMecanim.Skeleton;
        var skeletonData = skeleton.Data;
        characterSkin = new Skin("Skin");
        if(charactersSkinsDictionary.ContainsKey(character))
        {
            characterSkin.AddSkin(skeletonData.FindSkin($"{charactersSkinsDictionary[character]}"));
        }
        skeleton.SetSkin(characterSkin);
        skeleton.SetToSetupPose();
    }

    private void FillSkinsDictionary()
    {
        charactersSkinsDictionary.Add(CharacterTypes.Female, CharacterSkins.Female);
        charactersSkinsDictionary.Add(CharacterTypes.Male, CharacterSkins.Male);
    }

    private void CharacterChanged_ExecuteReaction(CharacterTypes character)
    {
        SetSkin(character);
    }
}
