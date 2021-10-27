using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Utilities
{
    public static class Messages
    {
        // Messages.Category.NotFound()
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }

            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklenmiştir.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla veritabanından silinmiştir.";
            }
        }

        public static class Movie
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Makaleler bulunamadı.";
                return "Böyle bir makale bulunamadı.";
            }
            public static string Add(string movieTitle)
            {
                return $"{movieTitle} başlıklı makale başarıyla eklenmiştir.";
            }

            public static string Update(string movieTitle)
            {
                return $"{movieTitle} başlıklı makale başarıyla güncellenmiştir.";
            }
            public static string Delete(string movieTitle)
            {
                return $"{movieTitle} başlıklı makale başarıyla silinmiştir.";
            }
        }
    }
}
