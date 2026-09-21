using PrimeTween;
using TMPro;
using UnityEngine;

public class ClickVFXHandler : MonoBehaviour
{
    [SerializeField] private RectTransform vfxSpawnZone;
    [SerializeField] private Transform PC;
    [SerializeField] private GameObject vfxPrefab;

    [SerializeField] private float vfxSpeed = 5f;
    [SerializeField] private float vfxFlyStep = 10f;

    [SerializeField] private float pcRotationStep = 45f;
    [SerializeField] private float pcRotationTime = 0.5f;

    private TMP_Text vfxText;

    private ClickController ClickController;

    private Tween rotateTween;

    public void Initialize(ClickController clickController)
    {
        ClickController = clickController;

        vfxText = vfxPrefab.GetComponent<TMP_Text>();

        clickController.OnClick += VFXSpawn;
    }

    private void VFXSpawn(double value)
    {
        var spawnPosition = GetRandomPosition();

        var vfx = Instantiate(vfxPrefab, vfxSpawnZone);

        vfxText.text = $"+{value}";

        vfx.transform.localPosition = spawnPosition;

        Tween.PositionY(vfx.transform, vfx.transform.position.y + vfxFlyStep, duration: vfxSpeed).OnComplete(() => Destroy(vfx));

        var endRotation = new Vector3(0, 0, pcRotationStep);

        if (rotateTween.isAlive) rotateTween.Stop();

        rotateTween = Tween.LocalRotation(PC, Vector3.zero, endRotation, pcRotationTime, ease: Ease.InBack)
            .OnComplete(() => 
            { 
                Tween.LocalRotation(PC, endRotation, Vector3.zero, pcRotationTime / 2);
            });
    }

    private Vector2 GetRandomPosition()
    {
        var rect = vfxSpawnZone.rect;

        var randomX = Random.Range(rect.xMin, rect.xMax);
        var randomY = Random.Range(rect.yMin, rect.yMax);

        return new Vector2(randomX, randomY);
    }
}
