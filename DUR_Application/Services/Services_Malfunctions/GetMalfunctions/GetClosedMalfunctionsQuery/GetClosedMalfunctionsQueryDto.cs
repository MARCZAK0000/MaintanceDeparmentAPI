using DUR_Application.Entities;
using DUR_Application.Helper;
using DUR_Application.Model;
using FluentValidation;

namespace DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetClosedMalfunctionsQuery
{
    public class GetClosedMalfunctionsQueryDto
    {
        public string DateTimeStart { get; set; }

        public string DateTimeEnd { get; set; }

        public string? LaneNumber { get; set; } 
        
        public string SortBy { get; set; }
        
        public SortDirection SortDirection { get; set; }

    }


    public class GetCloseMalfunctionsQueryValidator : AbstractValidator<GetClosedMalfunctionsQueryDto>
    {
        private string[] sortByColumns = new string[] { nameof(MalfunctionRequest.CreatedTime), nameof(MalfunctionRequest.LayoverTime) }; 
        public GetCloseMalfunctionsQueryValidator(DatabaseContext databaseContext)
        {
            DateTime DateTimeStart = new DateTime();
            RuleFor(pr => pr.DateTimeStart)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    DateTimeStart = ConvertDate.ChangeToDateTime(value);
                    if (ConvertDate.ChangeToDateTime(value) <= DateTime.Parse("2020/01/01"))
                    {
                        context.AddFailure("DateTimeStart", "Change date to higher than 2020/01/01");
                    }
                });

            RuleFor(pr => pr.DateTimeEnd)
                .Custom((value, context) =>
                {
                    var DateTimeEnd = ConvertDate.ChangeToDateTime(value);
                    if(DateTimeStart> DateTimeEnd)
                    {
                        context.AddFailure("DateTimeEnd",$"Change date to higher than {DateTimeStart}");
                    }
                });

            RuleFor(pr => pr.SortDirection)
                .Must(value => value.ToString() == "DESC" || value.ToString() == "ASC")
                .WithMessage($"{nameof(SortDirection)}: has to be either ASC or DESC");

            RuleFor(pr => pr.SortBy)
                .Must(value=>string.IsNullOrEmpty(value) || sortByColumns.Contains(value));

            RuleFor(pr => pr.LaneNumber)
                .Must(value => string.IsNullOrEmpty(value) || databaseContext.Lanes.Any(pr => pr.Number == value))
                .WithMessage("Lane number has to be either null/empty or be in database");


        }
    }
}
