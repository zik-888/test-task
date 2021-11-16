using Enemy;
using UnityEngine;
using UnityEngine.UI;

internal class DetectedState : DetectableState
{
    public DetectedState(Image image, ILevelController levelController, EnemyMovement enemyMovement) : base(image, levelController, enemyMovement)
    {
        SetDetected();
    }

    public override bool ChangeState(Detectable detectable, float value) => false;

    private void SetDetected()
    {
        ValueIndicate = 1;
        LevelController.MissionEventInvoke(MissionPart.OneEnemyDestroy, EnemyMovement.gameObject);
        EnemyMovement.isFinding = false;
        EnemyMovement.OffendedLeave();
    }
}