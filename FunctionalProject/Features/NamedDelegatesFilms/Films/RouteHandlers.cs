﻿namespace FunctionalProject.Features.NamedDelegatesFilms.Films
{
    using FunctionalProject.Features.FuncFilms.UpdateFilm;
    using FunctionalProject.Features.NamedDelegatesFilms.CastMembers;
    using FunctionalProject.Features.NamedDelegatesFilms.Directors;
    using FunctionalProject.Features.NamedDelegatesFilms.Films.CreateFilm;
    using FunctionalProject.Features.NamedDelegatesFilms.Films.DeleteFilm;
    using FunctionalProject.Features.NamedDelegatesFilms.Films.ListFilmById;
    using FunctionalProject.Features.NamedDelegatesFilms.Films.ListFilms;
    using FunctionalProject.Features.NamedDelegatesFilms.Films.Permissions;

    public static class RouteHandlers
    {
        public static CreateFilmDelegate CreateFilmHandler;

        public static ListFilmByIdDelegate ListFilmByIdHandler;

        public static DeleteFilmDelegate DeleteFilmHandler;

        public static ListFilmsDelegate ListFilmsHandler;

        public static UpdateFilmDelegate UpdateFilmHandler;
        
        static RouteHandlers()
        {
            CreateFilmHandler = film => CreateFilmRoute.Handle(film, () => ValidUserQuery.Execute());

            DeleteFilmHandler = id => DeleteFilmRoute.Handle(id, () => ValidUserQuery.Execute());
            
            ListFilmByIdHandler = id => ListFilmByIdRoute.Handle(
                id,
                filmId => ListFilmsByIdQuery.ListFilmsByIdQuery.Execute(id),
                dirId => GetDirectorByIdQuery.Execute(dirId),
                filmId => GetCastByFilmIdQuery.Execute(id)
            );

            ListFilmsHandler = () => ListFilmsRoute.Handle();

            UpdateFilmHandler = (id, film) => UpdateFilmRoute.Handle(
                id, 
                film, 
                () => ValidUserQuery.Execute(), 
                filmId => ListFilmsByIdQuery.ListFilmsByIdQuery.Execute(filmId));
        }
    }
}
