using System;
using System.Linq;
using other;

public static class Flag_Extend
{
    public static bool HadFlag(this TileMapType flag,TileMapType otherFlag)
    {
        return (flag & otherFlag) != 0;
    }

    public static TileMapType AddFlag(this ref TileMapType flag,TileMapType otherFlag)
    {
        flag |= otherFlag;
        return flag;
    }
    
    public static TileMapType RemoveFlag(this ref TileMapType flag,TileMapType otherFlag)
    {
        flag &= ~otherFlag;
        return flag;
    }
    
    public static int FlagIndex(this TileMapType flag)
    {
        var i = 0;
        
        foreach (TileMapType tileMapType in Enum.GetValues(typeof(TileMapType)))
        {
            if (tileMapType == flag)
            {
                return i;
            }
            i++;
        }

        return -1;

        // int NumOnes = 0;
        // int number = (int)flag;
        // while(number!=0){
        //     if((number & 1) != 0)
        //         NumOnes++;
        //     number = number >> 1;
        // }
        //
        // return NumOnes;
    }
    private static bool JustOneFlag(this TileMapType flag)
    {
        return Enum.GetValues(typeof(TileMapType)).Cast<TileMapType>().Any(tileMapType => flag == tileMapType);
    }
    
    public static bool RealType(this TileMapType tileMapType)
    {
        return tileMapType != TileMapType.None && tileMapType.JustOneFlag();
    }
    
}