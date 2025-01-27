using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitMenu : MonoBehaviour
{
    protected List<Define.MenuType> previousMenus;
    [SerializeField] protected MainGameMenuManager mainGameMenuManager;
    protected UnitController menuUnit;
    protected Vector3 menuPosition;

    public abstract void OpenMenu(Define.MenuType menuToOpen, List<Define.MenuType> previousMenus, UnitController unit, Vector3 position);

    public abstract void CloseMenu();

    public abstract void CloseToPreviousMenu();
}
