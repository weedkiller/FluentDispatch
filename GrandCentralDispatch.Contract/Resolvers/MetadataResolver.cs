﻿using System.Linq;
using MagicOnion;
using Microsoft.Extensions.Logging;
using GrandCentralDispatch.Contract.Models;
using GrandCentralDispatch.Models;
using GrandCentralDispatch.Resolvers;
using Microsoft.Extensions.Configuration;
using TMDbLib.Client;

namespace GrandCentralDispatch.Contract.Resolvers
{
    public sealed class MetadataResolver : Item1RemotePartialResolver<MovieDetails, MovieDetails>
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public MetadataResolver(ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<MetadataResolver>();
            _configuration = configuration;
        }

        /// <summary>
        /// Process each new payload
        /// </summary>
        /// <param name="movieDetails"><see cref="MovieDetails"/></param>
        /// <param name="nodeMetrics"><see cref="NodeMetrics"/></param>
        /// <returns><see cref="UnaryResult{TResult}"/></returns>
        public override async UnaryResult<MovieDetails> ProcessItem1Remotely(MovieDetails movieDetails,
            NodeMetrics nodeMetrics)
        {
            _logger.LogInformation(
                $"Movie title received from node {nodeMetrics.Id}: {movieDetails.Title}.");
            var client = new TMDbClient(_configuration["TMDB_API_KEY"]);
            var movie = await client.SearchMovieAsync(movieDetails.Title);
            return new MovieDetails
            {
                Title = movieDetails.Title,
                Overview = movie.Results.First().Overview
            };
        }
    }
}