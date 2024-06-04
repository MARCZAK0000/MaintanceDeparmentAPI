using DUR_Application.Entities;
using DUR_Application.Helper;
using DUR_Application.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage;

namespace DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetAllMalfunctionsQuery
{
    public class GetAllMalfunctionsQuery
    {
        public string DateTimeStart { get; set; }

        public string DateTimeEnd { get; set; }

        public StatusOfRequest StatusOfRequest { get; set; }
        
        public string? LaneNumber { get; set; }

        public string SortBy { get; set; }  

        public SortDirection SortDirection { get; set; }

    }

    public class GetAllMalfucntionsQueryValidation : AbstractValidator<GetAllMalfunctionsQuery>
    {
        private string[] sortByColumns = new string[] { nameof(MalfunctionRequest.CreatedTime), nameof(MalfunctionRequest.RequestType.Name) };
        public GetAllMalfucntionsQueryValidation(DatabaseContext databaseContext)
        {
            var DateTimeStart = new DateTime();

            RuleFor(pr => pr.DateTimeStart)
                .Custom((value, context)=>
                {
                    DateTimeStart = ConvertDate.ChangeToDateTime(value);
                    var minDate = ConvertDate.ChangeToDateTime("2020/01/01");
                    if (DateTimeStart < minDate)
                    {
                        context.AddFailure($"{nameof(DateTimeStart)}", "Invalid Date");
                    }
                });


            RuleFor(pr => pr.DateTimeEnd)
                .Custom((value, context) =>
                {
                    var DateTimeEnd = ConvertDate.ChangeToDateTime(value);

                    if (DateTimeEnd < DateTimeStart)
                    {
                        context.AddFailure($"{nameof(DateTimeEnd)}", "Invalid Date");
                    }
                });


            RuleFor(pr => pr.SortDirection)
                .IsInEnum();


            RuleFor(pr => pr.StatusOfRequest)
                .Must(value => value.ToString() == "Open" || value.ToString() == "Pending")
                .WithMessage("Status of Request has to be Open or Pending");

            RuleFor(pr => pr.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || sortByColumns.Contains(value));

            RuleFor(pr => pr.LaneNumber)
                .Must(value => string.IsNullOrEmpty(value) || databaseContext.Lanes.Any(pr => pr.Number == value))
                .WithMessage("Lane number has to be either null a null/empty or valid in database");
        }
    }
}
