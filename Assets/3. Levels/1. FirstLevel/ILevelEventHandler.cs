using UnityEngine;

public interface ILevelEventHandler
{
    void MissionEventInvoke(MissionPart missionPart, GameObject obj);
}