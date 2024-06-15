using ZXing.Common;

namespace ZXing
{
    internal class BarcodeWriter
    {
        public Net.Maui.BarcodeFormat Format { get; set; }
        public EncodingOptions Options { get; set; }
    }
}