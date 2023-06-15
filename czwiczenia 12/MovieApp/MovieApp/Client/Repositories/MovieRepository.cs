using MovieApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MovieApp.Client.Helpers.MovieService;
using static MovieApp.Client.Repositories.MovieRepository;

namespace MovieApp.Client.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public interface IMovieRepository
        {
            Task<int> AddMovie(Movie movie);
            Task DeleteMovie(int Id);
            Task<Movie> GetMovie(int id);
            Task<List<Movie>> GetMovies();
            Task UpdateMovie(Movie movie);
        }

        private readonly IMovieService movieService;
        private string url = "api/movies";

        public MovieRepository(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<int> AddMovie(Movie movie)
        {
            var response = await movieService.Post<Movie, int>(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task DeleteMovie(int Id)
        {
            var response = await movieService.Delete($"{url}/{Id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<Movie> GetMovie(int id)
        {

            return (await movieService.Get<Movie>($"{url}/{id}")).Response;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return (await movieService.Get<List<Movie>>(url)).Response;
        }

        public async Task UpdateMovie(Movie movie)
        {
            var response = await movieService.Put(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

      
    }
}
