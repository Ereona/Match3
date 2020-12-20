using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public int RowsCount;
    public int ColsCount;
    public int HolesCount;
    public FieldBuilder Builder;
    public GameStateController StateController;

    private void Start()
    {
        FieldGenerator generator = new FieldGenerator();
        List<CellGenerationInfo> generationInfos = new List<CellGenerationInfo>();
        CellGenerationInfo holesInfo = new CellGenerationInfo();
        holesInfo.type = CellType.Hole;
        holesInfo.fixedCount = true;
        holesInfo.count = HolesCount;
        generationInfos.Add(holesInfo);
        CellGenerationInfo spawnerInfo = new CellGenerationInfo();
        spawnerInfo.type = CellType.Spawner;
        spawnerInfo.fixedRow = true;
        spawnerInfo.rowNumber = 0;
        generationInfos.Add(spawnerInfo);
        Field field = generator.Generate(RowsCount, ColsCount, generationInfos);

        List<Cell> allCells = field.GetAllCells();
        List<Cell> fillingCells = allCells.Where(c => GameRules.NeedFillCell(c.type)).ToList();
        GemsGenerator gemsGenerator = new GemsGenerator();
        int[] colors = new int[] { 0, 1, 2, 3 };
        GemGenerationSettings gemsGenerationInfos = new FixedSizeRandomGemGenerationSettings(colors, fillingCells.Count);
        List<Gem> gems = gemsGenerator.Generate(fillingCells, gemsGenerationInfos);

        Builder.BuildField(field);

        StateController.Init(field, colors);
    }
}
