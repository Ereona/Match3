using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public GameSettingsSO Settings;
    public FieldBuilder Builder;
    public GameStateController StateController;

    private void Start()
    {
        FieldGenerator generator = new FieldGenerator();
        List<CellGenerationInfo> generationInfos = new List<CellGenerationInfo>();
        CellGenerationInfo holesInfo = new CellGenerationInfo();
        holesInfo.type = CellType.Hole;
        holesInfo.fixedCount = true;
        holesInfo.count = Settings.HolesCount;
        generationInfos.Add(holesInfo);
        CellGenerationInfo spawnerInfo = new CellGenerationInfo();
        spawnerInfo.type = CellType.Spawner;
        spawnerInfo.fixedRow = true;
        spawnerInfo.rowNumber = 0;
        generationInfos.Add(spawnerInfo);
        Field field = generator.Generate(Settings.RowsCount, Settings.ColsCount, generationInfos);

        List<Cell> allCells = field.GetAllCells();
        List<Cell> fillingCells = allCells.Where(c => GameRules.NeedFillCell(c.type)).ToList();
        GemsGenerator gemsGenerator = new GemsGenerator();
        GemGenerationSettings gemsGenerationInfos = new FixedSizeRandomGemGenerationSettings(Settings.Colors, fillingCells.Count);
        List<Gem> gems = gemsGenerator.Generate(fillingCells, gemsGenerationInfos);

        Builder.BuildField(field);

        StateController.Init(field, gemsGenerator, Settings.Colors);
    }
}
