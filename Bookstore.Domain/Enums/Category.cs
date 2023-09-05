using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Enums
{
    public enum Category
    {
        [Display(Name = "Художественная литература")]
        FictionLiterature = 1,

        [Display(Name = "Детская литература")]
        ChildrenLiterature = 2,

        [Display(Name = "Научно-популярная литература")]
        ScienceLiterature = 3,

        [Display(Name = "Учебная литература")]
        EducationalLiterature = 4,

        [Display(Name = "Техническая литература")]
        TechnicalLiterature = 5,

        [Display(Name = "Религиозная литература")]
        ReligiousLiterature = 6,

        [Display(Name = "Литература для взрослых")]
        AdultLiterature = 7
    }
}
