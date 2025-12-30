using System.Collections.Generic;
using System.Linq;
using WebsiteQuanLyDinhDuongCaNhan.Models;

public class MealOptimizationService
{
    public List<Food> OptimizeMealPlan(List<Food> foodList, double calorieTarget)
    {
        return foodList
            .Where(f => f.Calories < calorieTarget / 3)
            .OrderByDescending(f => f.Protein)
            .Take(3)
            .ToList();
    }
}
