using FastEndpoints;
using FluentValidation;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V2.User.GetList;
public class GetUserListRequestValidator : Validator<GetUserListRequest>
{
    public GetUserListRequestValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage("Query cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Query cannot exceed 100 characters.")
            .MinimumLength(3)
            .WithMessage("Query must be at least 3 characters long");

        RuleFor(x => x.Page)
            .GreaterThan((uint)0)
            .WithMessage("Page must be greater than 0")
            .LessThanOrEqualTo((uint)100)
            .WithMessage("Page must be less than or equal to 100");

        RuleFor(x => x.Limit)
            .InclusiveBetween((uint)1, (uint)100).WithMessage("Limit must be between 1 and 100.");
    }
}
