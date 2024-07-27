using Blog.Entity.Entities;
using FluentValidation;

namespace Blog.Service.FluentValidations;

public class UserValidator : AbstractValidator<AppUser>
{
    public UserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email cannot be null.")
            .MinimumLength(5).WithMessage("Email must be at least 5 characters long.")
            .EmailAddress().WithMessage("Email format is not valid."); 

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .NotNull().WithMessage("Phone number cannot be null.")
            .Matches(@"^\+?(\d{10,15})$").WithMessage("Phone number format is not valid.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name cannot be null.")
            .MaximumLength(30).WithMessage("Name of employee can max 30 characters.");
         
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required.")
            .NotNull().WithMessage("Lastname cannot be null.")
            .MaximumLength(30).WithMessage("Lastname of employee can max 30 characters.");
    }
}