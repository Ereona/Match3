using System.Collections;
using System.Collections.Generic;
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
    }
}
