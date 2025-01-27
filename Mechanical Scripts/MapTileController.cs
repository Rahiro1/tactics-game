using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapTileController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    public Define.TileType tileType { get; private set; }
    public Vector3Int MapLocation { get; set; }
    public bool occupied;
    public List<int> eRangeHighlightIDList = new List<int>();
    // TODO variable for determining terrain difficulty
    //public Define.TerrainDifficultyValues TerrainDifficultyValues;
    [SerializeField] private SpriteRenderer tileSpriterenderer;
    [SerializeField] private SpriteRenderer cursorImage;
    [SerializeField] private SpriteRenderer highlightImage;
    [SerializeField] private SpriteRenderer highlightImageEnemyRange;
    [SerializeField] private SpriteRenderer aestheticTile;
    
    public int AvoidValue
    {
        get
        {
            return Database.Instance.tileMoveDictionary[tileType].bonusAvoid;
        }
    }
    public int GuardValue
    {
        get
        {
            return Database.Instance.tileMoveDictionary[tileType].bonusGuard;
        }
    }
    public int RegenerationValue
    {
        get
        {
            return Database.Instance.tileMoveDictionary[tileType].bonusRegeneration;
        }
    }
    private UnitController occupyingUnit;

    // A* Pathfinding variables
    public int aStarG;
    public int aStarH;
    public int aStarF;
    public MapTileController Previous { get; set; }
    // rangefind variables
    public int cumulativeMoveCost { get; private set; }

    #region "Unit Change Methods"
    public void SetUnit(UnitController unit)
    {
        if(unit != null)
        {
            occupyingUnit = unit;
            occupied = true;
        }
        else
        {
            occupyingUnit = null;
            occupied = false;
        }
    }

    public void RemoveUnit()
    {
        occupyingUnit = null;
        occupied = false;
    }

    public UnitController getUnit()
    {
        return occupyingUnit;
    }
    #endregion


    public void InitialiseTile(Sprite sprite, Define.TileType tileType, Color color, float rotationAmount, Sprite aestheticTileSprite)
    {
        this.tileType = tileType;
        tileSpriterenderer.sprite = sprite;
        tileSpriterenderer.color = color;
        tileSpriterenderer.gameObject.transform.Rotate(new Vector3(0, 0, rotationAmount));
        if(aestheticTileSprite != null)
        {
            aestheticTile.sprite = aestheticTileSprite;
        }
        else
        {
            aestheticTile.sprite = null;
        }
        
    }

    public void ChangeAestheticTile(Sprite newSprite)
    {
        aestheticTile.sprite = newSprite;
    }

    public int GetMoveCost(Define.UnitType unitType)
    {
        switch (unitType)
        {
            case Define.UnitType.Foot:
                return Database.Instance.tileMoveDictionary[tileType].footMoveCost;
            case Define.UnitType.Armour:
                return Database.Instance.tileMoveDictionary[tileType].armourMoveCost;
            case Define.UnitType.Water:
                return Database.Instance.tileMoveDictionary[tileType].waterMoveCost;
            case Define.UnitType.Flying:
                return Database.Instance.tileMoveDictionary[tileType].flyMoveCost;
            case Define.UnitType.Beast:
                return Database.Instance.tileMoveDictionary[tileType].beastMoveCost;
            default:
                Debug.LogError("Unit Type not present in dictionary");
                return 100;
        }
    }

    public void CalcNewCumulativeMoveCost(int costOfPrevious, Define.UnitType unitType)
    {
        cumulativeMoveCost = Mathf.Min(cumulativeMoveCost, costOfPrevious + GetMoveCost(unitType));
    }

    public void InitialiseCumulativeMoveStartTile()
    {
        cumulativeMoveCost = 0;
    }

    public void InitialiseCumulativeMoveNotStartTile()
    {
        cumulativeMoveCost = 100;
    }


    #region "Highlight Methods"
    // CONSIDER adding red/blue highlight options
    public void Highlight()
    {
        highlightImage.enabled = true;
    }

    public void UnHighlight()
    {
        highlightImage.enabled = false;
    }

    public void SoftHighlight()  // will be used to highlight battalion
    {

    }

    public void UnSoftHighlight() // will be used to unhighlight battalion
    {

    }

    public void HighlightAttackRange(int ID)
    {
        eRangeHighlightIDList.Add(ID);
        highlightImageEnemyRange.enabled = true;
    }

    public void UnHighlightAttackRange(int ID)
    {
        eRangeHighlightIDList.Remove(ID); // does this need to be null checked?
        if (eRangeHighlightIDList.Count == 0)
        {
            highlightImageEnemyRange.enabled = false;
        }
    }

    #endregion





    // methods for mouse events
    #region "Mouse Events"
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorImage.enabled = true;
        if (occupied)
        {
            gameManager.statusWindowManager.OpenMenu(occupyingUnit, gameObject.transform.position + 2* Vector3.up);
            if (occupyingUnit.Character.unitAllignment == Define.UnitAllignment.Enemy)
            {
                gameManager.HighlightBattalion(occupyingUnit);
            }
        }

        gameManager.terrainTooltip.ShowTooltip(Define.TileTypeToText(tileType), AvoidValue, GuardValue, RegenerationValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorImage.enabled = false;
        if(occupied)
        {
            gameManager.statusWindowManager.CloseMenu();
            if (occupyingUnit.Character.unitAllignment == Define.UnitAllignment.Enemy)
            {
                gameManager.UnHighlightBattalion(occupyingUnit);
            }
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!occupied)
            {
                return;
            }
            gameManager.OnRightClickUnit(occupyingUnit);
        }


        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (occupied)
        {
            

            if (occupyingUnit.Character.unitAllignment == Define.UnitAllignment.Player)
            {
                gameManager.OnPlayerClicked(occupyingUnit);
            }else if (occupyingUnit.Character.unitAllignment == Define.UnitAllignment.Enemy)
            {
                gameManager.OnEnemyClicked(occupyingUnit);
            }
            
        }
        else
        {
            gameManager.OnTileClicked(this);
        }
    }
    #endregion
}
