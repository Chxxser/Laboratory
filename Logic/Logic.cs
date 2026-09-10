using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Logic
    {
        public List<Phone> phones = new List<Phone>();
        public int _Id = 1;
        public Phone AddPhone(string brand, string model, int year, string color, decimal price, int memory, bool availability)
        {
            var phone = new Phone()
            {
                Id = _Id++,
                Brand = brand,
                Model = model,
                Year = year,
                Color = color,
                Price = price,
                Memory = memory,
                Availability = availability
            };
            phones.Add(phone);
            return phone;
        }
        public List<Phone> AllPhone() { return phones.ToList(); }
        public Phone PhoneId(int id)
        {
            foreach (Phone i in phones)
            {
                if (i.Id == id) { return i; }
            }
            return null;
        }
        public bool UpdatePhone(int id, string brand, string model, int year, string color, decimal price, int memory, bool availability)
        {
            Phone phone = PhoneId(id);
            if (phone == null) return false;
            phone.Brand = brand;
            phone.Model = model;
            phone.Year = year;
            phone.Color = color;
            phone.Price = price;
            phone.Memory = memory;
            phone.Availability = availability;
            return true;
        }
        public bool DeletePhone(int id)
        {
            Phone phone = PhoneId(id);
            if (phone == null) return false;
            return phones.Remove(phone);
        }
        public Dictionary<int, List<Phone>> GroupMemory()
        {
            Dictionary<int, List<Phone>> result = new Dictionary<int, List<Phone>>();
            foreach (Phone i in phones)
            {
                if (result.ContainsKey(i.Memory) == false) { result[i.Memory] = new List<Phone>(); }
                result[i.Memory].Add(i);
            }
            return result;
        }
        public List<Phone> ExpensivePhones(int count)
        {
            List<Phone> sorted = new List<Phone>();
            foreach (Phone i in phones) { sorted.Add(i); }
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = 0; j < sorted.Count - i - 1; j++)
                {
                    if (sorted[j].Price < sorted[j + 1].Price)
                    {
                        Phone a = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = a;
                    }
                }
            }
            List<Phone> result = new List<Phone>();
            int limit;
            if (count < sorted.Count) { limit = count; }
            else { limit = sorted.Count; }
            for (int i = 0; i < limit; i++) { result.Add(sorted[i]); }
            return result;
        }
        public void TestPhone()
        {
            AddPhone("Apple", "iPhone 15", 2023, "Black", 100000, 128, true);
            AddPhone("Apple", "iPhone 15 Pro Max", 2023, "Titanium", 150000, 256, true);
            AddPhone("Samsung", "Galaxy S24", 2024, "White", 90000, 256, true);
            AddPhone("Samsung", "Galaxy Z Fold 5", 2023, "Blue", 180000, 512, false);
            AddPhone("Xiaomi", "Redmi Note 13", 2024, "Green", 35000, 128, true);
            AddPhone("Xiaomi", "Mi 14", 2023, "Black", 70000, 256, true);
            AddPhone("Google", "Pixel 8 Pro", 2023, "Porcelain", 110000, 256, true);
            AddPhone("OnePlus", "12", 2024, "Emerald", 80000, 512, false);
            AddPhone("Huawei", "P60 Pro", 2023, "Silver", 95000, 256, true);
            AddPhone("Nokia", "G42", 2023, "Purple", 25000, 128, true);
        }
    }
}
