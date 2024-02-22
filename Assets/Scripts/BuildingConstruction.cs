using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour {

    private const string CONSTRUCTION_MATERIAL_PROGRESS = "_Progress";

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject buildingPlacedParticle;
    private BuildingSO building;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider;
    private Material constructionMaterial;

    public void SetBuilding(BuildingSO building) {
        this.building = building;

        GetComponent<BuildingBase>().SetBuilding(building);

        spriteRenderer.sprite = building.Sprite;

        constructionTimerMax = building.ConstructionTimerMax;
        constructionTimer = building.ConstructionTimerMax;

        BoxCollider2D buildingBoxCollider = building.Prefab.GetComponent<BoxCollider2D>();
        boxCollider.size = buildingBoxCollider.size;
        boxCollider.offset = buildingBoxCollider.offset;
    }

    public float GetConstructionProgressNormalized() {
        return 1 - constructionTimer / constructionTimerMax;
    }

    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        constructionMaterial = spriteRenderer.material;
        Instantiate(buildingPlacedParticle, transform.position, Quaternion.identity);
    }

    private void Update() {
        constructionTimer -= Time.deltaTime;
        constructionMaterial.SetFloat(CONSTRUCTION_MATERIAL_PROGRESS, GetConstructionProgressNormalized());
        if (constructionTimer <= 0) {
            constructionTimer = constructionTimerMax;
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Instantiate(building.Prefab, transform.position, Quaternion.identity);
            Instantiate(buildingPlacedParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
