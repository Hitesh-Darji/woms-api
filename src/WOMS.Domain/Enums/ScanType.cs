using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum ScanType
    {
        [Description("Barcode")]
        Barcode = 0,
        [Description("QR Code")]
        QRCode = 1,
        [Description("RFID")]
        RFID = 2,
        [Description("Manual")]
        Manual = 3,
        [Description("Other")]
        Other = 4,
    }
}
