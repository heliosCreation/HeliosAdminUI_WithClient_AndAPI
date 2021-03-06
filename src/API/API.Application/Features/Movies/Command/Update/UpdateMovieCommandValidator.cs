using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Features.Movies.Command.Update;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Command.Create
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateMovieCommandValidator(
            IMovieRepository movieRepository,
            ILoggedInUserService loggedInUserService,
            ICategoryRepository categoryRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

            RuleFor(m => m.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(0, 120).WithMessage("{PropertyName} can't exceed 120 characters");
            

            RuleFor(m => m.Description)
                .Length(0, 500).WithMessage("{PropertyName} can't exceed 500 characters");


            RuleFor(m => m)
                .MustAsync(IsMovieNameUniqueForUserAndCategoryOnUpdate).WithMessage("A movie with this name and category is already recorded for your account.");

            RuleFor(m => m)
                .MustAsync(IsCategoryValid).WithMessage("The entered category does not exist.");
        }

        private async Task<bool> IsMovieNameUniqueForUserAndCategoryOnUpdate(UpdateMovieCommand e, CancellationToken c)
        {
            return await _movieRepository.IsMovieNameUniqueForUserAndCategoryOnUpdate(e.Name,e.Id, _loggedInUserService.UserId, e.CategoryId);
        }
        private async Task<bool> IsCategoryValid(UpdateMovieCommand e, CancellationToken c)
        {
            return await _categoryRepository.CategoryExist(e.CategoryId);
        }
    }
}
