namespace Logic
{
    /// <summary>
    /// Класс для генерации тестовых данных
    /// </summary>
    public class DataSeeder
    {
        public IPhoneService _phoneService;

        /// <summary>
        /// Конструктор генератора тестовых данных
        /// </summary>
        /// <param name="phoneService">Сервис для добавления телефонов</param>
        public DataSeeder(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        /// <summary>
        /// Заполнить базу данных тестовыми телефонами
        /// </summary>
        public void Seed()
        {
            _phoneService.AddPhone("Apple", "iPhone 15", 2023, "Black", 100000, 128, true);
            _phoneService.AddPhone("Apple", "iPhone 15 Pro Max", 2023, "Titanium", 150000, 256, true);
            _phoneService.AddPhone("Samsung", "Galaxy S24", 2024, "White", 90000, 256, true);
            _phoneService.AddPhone("Samsung", "Galaxy Z Fold 5", 2023, "Blue", 180000, 512, false);
            _phoneService.AddPhone("Xiaomi", "Redmi Note 13", 2024, "Green", 35000, 128, true);
            _phoneService.AddPhone("Xiaomi", "Mi 14", 2023, "Black", 70000, 256, true);
            _phoneService.AddPhone("Google", "Pixel 8 Pro", 2023, "Porcelain", 110000, 256, true);
            _phoneService.AddPhone("OnePlus", "12", 2024, "Emerald", 80000, 512, false);
            _phoneService.AddPhone("Huawei", "P60 Pro", 2023, "Silver", 95000, 256, true);
            _phoneService.AddPhone("Nokia", "G42", 2023, "Purple", 25000, 128, true);
        }
    }
}