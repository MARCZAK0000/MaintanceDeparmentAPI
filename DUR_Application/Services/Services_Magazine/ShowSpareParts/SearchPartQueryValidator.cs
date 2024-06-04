using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_Magazine.ShowSpareParts
{
    public class SearchPartQueryValidator : AbstractValidator<SearchPartQuery>
    {
        private int[] array = new int[] { 5, 10, 15 };

        private string[] sortByColumName = new string[] { nameof(SparePart.Name), nameof(SparePart.Type), nameof(SparePart.Price) };
        public SearchPartQueryValidator()
        {


            RuleFor(pr => pr.PageNumber)
                .GreaterThan(0);

            RuleFor(pr => pr.PageSize)
                .Custom((value, context) =>
                {
                    if (!array.Contains(value))
                    {
                        context.AddFailure("PageSize", "Page size must be [5,10,15]");
                    }
                });

            RuleFor(pr => pr.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || sortByColumName.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", sortByColumName)}]");

            RuleFor(pr => pr.SortDirection)
                .IsInEnum();

                
        }
    }
}
