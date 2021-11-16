using Enemy;
using UnityEngine;
using UnityEngine.UI;

internal class DetectedState : DetectableState
{
    public DetectedState(Image image, ILevelEventHandler levelEventHandler, EnemyMovement enemyMovement) : base(image, levelEventHandler, enemyMovement)
    {
        SetDetected();
    }

    public override bool ChangeState(Detectable detectable, float value) => false;

    private void SetDetected()
    {
        ValueIndicate = 1;
        LevelEventHandler.MissionEventInvoke(MissionPart.OneEnemyDestroy, EnemyMovement.gameObject);
        EnemyMovement.isFinding = false;
        EnemyMovement.OffendedLeave();
    }
}