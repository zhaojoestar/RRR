using UnityEngine;

public class TriggerToggle : MonoBehaviour
{
    public GameObject Obj;
    public Collider Sensor;
    public Emitter Emitter;
    public Transform Target;

    public void Show()
    {
        Obj.SetActive(true);
    }

    public void Hide()
    {
        Obj.SetActive(false);
    }

    public void Open()
    {
        Sensor.enabled = true;
    }

    public void Close()
    {
        Sensor.enabled = false;
    }

    public void Emit()
    {
        Emitter.Emit(Target);
    }
}
