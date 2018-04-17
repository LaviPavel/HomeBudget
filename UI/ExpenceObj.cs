using System.Collections.Generic;

namespace UI
{
    public class ExpenceObj
    {
        public string CategoryName { get; set; }
        private List<SubCategory> _subCategories = new List<SubCategory>();

    public ExpenceObj()
        {
        }
        public ExpenceObj(string categotyName)
        {
            CategoryName = categotyName;
        }

        public void AddSubCategory(string name, int expectedAmount)
        {
            _subCategories.Add(new SubCategory(name, expectedAmount));
        }

        public List<SubCategory> GetAllSubCategory()
        {
            return _subCategories;
        }

        public void UpdateCategoryName(string categotyNewName)
        {
            CategoryName = categotyNewName;
        }
        public int GetExpectedValueByName(string subCategoryName)
        {
            foreach (var item in _subCategories)
            {
                if (item.SubCategoryName == subCategoryName)
                {
                    return item.ExpectedAmount;
                }
            }
            return -1;
        }

        public void SetExpectedValueByName(string subCategoryName, int newExpectedValue)
        {
            foreach (var item in _subCategories)
            {
                if (item.SubCategoryName == subCategoryName)
                {
                    item.ExpectedAmount = newExpectedValue;
                }
            }
        }
        public void SetActualValueByName(string subCategoryName, int newActualValue)
        {
            foreach (var item in _subCategories)
            {
                if (item.SubCategoryName == subCategoryName)
                {
                    item.ActoualAmount = newActualValue;
                }
            }
        }

    }

    public class SubCategory
    {
        public string SubCategoryName { get; set; }
        public int ExpectedAmount { get; set; }
        public int ActoualAmount { get; set; }

        public SubCategory(string name, int expectedAmount, int actoualAmount = 0)
        {
            SubCategoryName = name;
            ExpectedAmount = expectedAmount;
            ActoualAmount = actoualAmount;
        }
    }
}
