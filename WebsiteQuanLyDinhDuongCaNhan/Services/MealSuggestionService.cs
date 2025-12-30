using System.Collections.Generic;
using System.Linq;

public class MealSuggestionService
{
    private List<string> foodHistory = new List<string> { "Salad", "Chicken", "Rice", "Fish", "Vegetables" };

    public string SuggestMeal()
    {
        return foodHistory.OrderBy(f => new System.Random().Next()).First();
    }
}
