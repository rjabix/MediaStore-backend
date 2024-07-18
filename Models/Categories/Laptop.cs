namespace MediaStore_backend.Models.Categories
{
    public class Laptop : Product
    {
        public string? Brand { get; set; } //Samsung, Apple, Xiaomi, etc.
        public string? Color { get; set; }
        public string? StorageType { get; set; } //ssd
        public string? StorageSize { get; set; } //gb
        public string? Display { get; set; } //oled, amoled, etc.
        public string? Resolution { get; set; } //1920x1080, etc.
        public string? Battery { get; set; } //mAh
        public float? ScreenSize { get; set; } //inches
        public List<string>? Ports { get; set; } //lightning, usb-c, etc.
        public string? Features { get; set; } //fingerprint, face unlock, etc.
        public string? Weight { get; set; } //grams
        public string? Material { get; set; } //plastic, glass, etc.

        //for product page
    }
}
