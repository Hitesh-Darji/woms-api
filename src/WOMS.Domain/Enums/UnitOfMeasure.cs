using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum UnitOfMeasure
    {
        [Description("Each")]
        Each = 0,
        [Description("Piece")]
        Piece = 1,
        [Description("Box")]
        Box = 2,
        [Description("Pallet")]
        Pallet = 3,
        [Description("Kilogram")]
        Kilogram = 4,
        [Description("Pound")]
        Pound = 5,
        [Description("Liter")]
        Liter = 6,
        [Description("Gallon")]
        Gallon = 7,
        [Description("Meter")]
        Meter = 8,
        [Description("Foot")]
        Foot = 9,
    }
}
