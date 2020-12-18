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

    private void Start()
    {
        FieldGenerator generator = new FieldGenerator();
        List<CellGenerationInfo> generationInfos = new List<CellGenerationInfo>();
        CellGenerationInfo holesInfo = new CellGenerationInfo();
        holesInfo.type = CellType.Hole;
        holesInfo.count = HolesCount;
        generationInfos.Add(holesInfo);
        Field field = generator.Generate(RowsCount, ColsCount, generationInfos);
        Builder.BuildField(field);

        List<Cell> allCells = field.GetAllCells();
        List<Cell> fillingCells = allCells.Where(c => GameRules.NeedFillCell(c.type)).ToList();
        GemsGenerator gemsGenerator = new GemsGenerator();
        int[] colors = new int[] { 0, 1, 2, 3 };
        GemGenerationSettings gemsGenerationInfos = new RandomGemGenerationSettings(colors, fillingCells.Count);
        List<Gem> gems = gemsGenerator.Generate(fillingCells, gemsGenerationInfos);
        Builder.BuildGems(gems);
    }
}
