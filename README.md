# WeatherDesk – Desktop Weather Application (WPF + C#)

WeatherDesk היא אפליקציית WPF שולחנית מתקדמת להצגת מידע מטאורולוגי בזמן אמת, כולל טמפרטורה, תיאור מצב מזג האוויר, תחזית לימים הקרובים ונתונים נוספים — עם ממשק גרפי מודרני וידידותי.

האפליקציה שואבת נתונים מממשק API חיצוני (OpenWeatherMap) ומציגה אותם בצורה ברורה, אלגנטית ומהירה.

---

## 🌦️ תכונות מרכזיות

- הצגת מזג אוויר בזמן אמת לפי מיקום
- טעינת נתונים מ־API חיצוני
- תמיכה בתצוגה גרפית מודרנית (WPF + XAML)
- עיבוד נתוני JSON והמרתם למודלים פנימיים
- טיפול מלא בשגיאות רשת / מפתחות API לא תקינים
- ממשק קליל, מהיר וידידותי

---

## 🛠️ טכנולוגיות

| תחום | טכנולוגיה |
|------|------------|
| Desktop UI | WPF (XAML) |
| Backend Logic | C# .NET |
| API Client | HttpClient |
| JSON Parsing | Newtonsoft.Json |
| Caching | Local Memory |
| Architecture | MVVM Design Pattern |

---

## 📦 מבנה הפרויקט (Project Structure)

הפרויקט בנוי לפי תבנית **MVVM** ומחולק לתיקיות ברורות:
WeatherDesk/
│
├── Models/ # מודלים עבור נתוני API
├── ViewModels/ # לוגיקה ופקודות (MVVM)
├── Views/ # קבצי XAML – ממשק המשתמש
├── Services/ # שירותי API וקוד חיצוני
└── Utils/ # עזרים, ממירים ועוד


---

## 🔄 מחזור פעולה (Workflow)

1. המשתמש מזין שם עיר  
2. ה־View שולח בקשה ל־ViewModel  
3. ה־ViewModel פונה לשירות `WeatherApiService`  
4. נשלחת בקשת HTTP אל OpenWeatherMap  
5. חוזר JSON → מומר למודלים  
6. ViewModel מעדכן את View  
7. התוצאה מוצגת בזמן אמת

---

## 🖼️ צילומי מסך (Screenshots)

> יש להחליף בתמונות אמיתיות מתוך האפליקציה:

### מסך ראשי
![Main Screen](assets/screenshot-main.png)

### תוצאות חיפוש
![Weather Result](assets/screenshot-weather.png)

### טעינת נתונים
![Loading Screen](assets/loading.png)

---

## 📥 הוראות התקנה והרצה

### 1. שכפול הריפו
```bash
git clone https://github.com/rikiMaman/weather-desk.git
cd weather-desk


2. פתיחה ב-Visual Studio

פתחי את WeatherDesk.sln

הריצי עם F5

3. הוספת API Key (חובה)

ב־Services/WeatherApiService.cs:
private readonly string _apiKey = "YOUR_API_KEY_HERE";

4. הפעלה

הזיני שם עיר

קבלי תחזית בזמן אמת

🧩 דוגמת קוד (Example Code)
public async Task<WeatherResponse> GetWeatherAsync(string city)
{
    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

    using var client = new HttpClient();
    var json = await client.GetStringAsync(url);

    return JsonConvert.DeserializeObject<WeatherResponse>(json);
}

📚 תיעוד API – OpenWeatherMap
Endpoint: מזג אוויר נוכחי
GET https://api.openweathermap.org/data/2.5/weather

פרמטרים
פרמטר	תיאור
q	שם העיר
appid	מפתח API
units	metric או imperial
דוגמת תגובה
{
  "weather": [
    { "description": "clear sky", "icon": "01d" }
  ],
  "main": { "temp": 29.4, "humidity": 65 },
  "wind": { "speed": 1.5 }
}

📁 מבנה קבצים מלא (Full File Structure)
WeatherDesk
│
├── WeatherDesk.sln
├── WeatherDesk/
│   ├── App.xaml
│   ├── MainWindow.xaml
│   ├── Models/
│   ├── ViewModels/
│   ├── Views/
│   ├── Services/
│   └── Utils/
└── README.md

👩‍💻 פיתוח ותרומה
git checkout -b feature/new-ui
git commit -am "Improved UI layout"
git push origin feature/new-ui

⭐ Credits

Developed by Rivka Maman
📧 Email: rivkim100@gmail.com
🔗 LinkedIn: https://www.linkedin.com/in/rivka-maman/
