using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }


    #region Header SPECIAL TILEMAP TILES
    [Space(10)]
    [Header("SPECIAL TILEMAP TILES")]
    #endregion Header SPECIAL TILEMAP TILES
    #region Tooltip
    [Tooltip("Collision tiles that the enemies can navigate to")]
    #endregion Tooltip
    public TileBase[] enemyUnwalkableCollisionTilesArray;
    #region Tooltip
    [Tooltip("Preferred path tile for enemy navigation")]
    #endregion Tooltip
    public TileBase preferredEnemyPathTile;

    #region Header MATERIALS
    [Space(10)]
    [Header("MATERIALS")]
    #endregion

    #region Tooltip
    [Tooltip("Sprite-Lit-Default Material")]
    #endregion
    public Material damegeFlashMaterial;

    #region Tooltip
    [Tooltip("Sprite-Lit-Default Material")]
    #endregion
    public Material litMaterial;
    #region Tooltip
    [Tooltip("Ammo Hit Effect Prefab")]
    #endregion
    public GameObject ammoHitEffectPrefab;

    #region Tooltip
    [Tooltip("Blood Effect Prefab")]
    #endregion
    public GameObject bloodEffectPrefab;

    #region Tooltip
    [Tooltip("Player Shadow Prefab")]
    #endregion
    public GameObject playerShadowPrefab;

    #region Tooltip
    [Tooltip("Begin UI Material")]
    #endregion
    public Material beginUI;

    #region Tooltip
    [Tooltip("Boss Material")]
    #endregion
    public Material boss;

    #region SOUNDS

    public AudioMixerGroup soundsMasterMixerGroup;
    public AudioMixerGroup musicMasterMixerGroup;
    public AudioMixerSnapshot musicLowSnapshot;
    public AudioMixerSnapshot musicOnFullSnapshot;
    public AudioMixerSnapshot musicOffSnapshot;

    public MusicTrackSO musicTrack1;
    public MusicTrackSO musicTrack2;
    public MusicTrackSO musicTrack_Bossfight;


    public SoundEffectSO rain;
    public SoundEffectSO getHurtEnemy;
    public SoundEffectSO getHurtPlayer;
    public SoundEffectSO clickButton;

    //Map
    public SoundEffectSO flowingWater;
    public SoundEffectSO waweOceanEffect;
    #endregion
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemyUnwalkableCollisionTilesArray), enemyUnwalkableCollisionTilesArray);
        HelperUtilities.ValidateCheckNullValue(this, nameof(preferredEnemyPathTile), preferredEnemyPathTile);
        HelperUtilities.ValidateCheckNullValue(this, nameof(damegeFlashMaterial), damegeFlashMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(litMaterial), litMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoHitEffectPrefab), ammoHitEffectPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(bloodEffectPrefab), bloodEffectPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerShadowPrefab), playerShadowPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(beginUI), beginUI);
        HelperUtilities.ValidateCheckNullValue(this, nameof(boss), boss);
        HelperUtilities.ValidateCheckNullValue(this, nameof(soundsMasterMixerGroup), soundsMasterMixerGroup);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicMasterMixerGroup), musicMasterMixerGroup);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicLowSnapshot), musicLowSnapshot);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicOnFullSnapshot), musicOnFullSnapshot);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicOffSnapshot), musicOffSnapshot);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicTrack1), musicTrack1);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicTrack2), musicTrack2);
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicTrack_Bossfight), musicTrack_Bossfight);
        HelperUtilities.ValidateCheckNullValue(this, nameof(rain), rain);
        HelperUtilities.ValidateCheckNullValue(this, nameof(getHurtEnemy), getHurtEnemy);
        HelperUtilities.ValidateCheckNullValue(this, nameof(getHurtPlayer), getHurtPlayer);
        HelperUtilities.ValidateCheckNullValue(this, nameof(clickButton), clickButton);
    }
#endif
    #endregion
}